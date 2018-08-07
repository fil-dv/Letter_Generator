using DbLayer;
using MyLetterManager.Repo;
using Oracle.ManagedDataAccess.Client;
using Semaphore.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ExcelLibrary.SpreadSheet;
using MyLetterManager.Infrastructure;

namespace MyLetterManager
{
    public static class PriorityManager
    {
        static OracleConnect _con;
        static public event Action<bool> UpdatePriorityCompleted;
        static public event Action<bool> FileLoadCompleted;

        public static void CreateConnect()
        {
            try
            {
                _con = new OracleConnect(AppSettings.DbConnectionString);
                _con.OpenConnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from MyLetterManager.LetterManager.CreateConnect()" + ex.Message);
            }
        }

        public static void ExecCommand(string command)
        {
            try
            {
                if (_con != null)
                {
                    _con.ExecCommand(command);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from MyLetterManager.PriorityManager.ExecCommand()" + ex.Message);
            }
        }

        static public void AddPinFromFile(List<Deal> insertList)
        {
            SetCtlFile();
            try
            {
                using (FileStream fs = new FileStream(@"Imp.csv", FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var item in insertList)
                    {
                        sw.WriteLine(item.DealId);
                    }
                }
                InsertToDbByBatFile();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла, попробуйте повторить попытку.", "Приоритеты", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PriorityLoger.AddRecordToLog("Не удается прочитать файл. " + ex.Message);
            }            
        }

        private static void SetCtlFile()
        {
            string text = "LOAD DATA " + Environment.NewLine +
                          "INFILE 'imp.csv' " + Environment.NewLine +
                          "REPLACE " + Environment.NewLine +
                          "INTO TABLE \"IMP_PRIOR\" " + Environment.NewLine +
                          "FIELDS TERMINATED BY ';' " + Environment.NewLine +
                          "TRAILING NULLCOLS " + Environment.NewLine +
                          "( " + Environment.NewLine +
                          "deal_id " + Environment.NewLine +
                          ")";
            File.WriteAllText("1_import.CTL", text);
        }

        private static void InsertToDbByBatFile()
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.Exited += new EventHandler(FileLoaded);
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = @"1_IMPORT.BAT";
                proc.EnableRaisingEvents = true;
                proc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи данных в базу, попробуйте повторить попытку.", "Приоритеты", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PriorityLoger.AddRecordToLog("Ошибка записи данных в базу. " + ex.Message);
            }            
        }

        static void FileLoaded(object sender, EventArgs e)
        {
            if (FileLoadCompleted != null)
            {
                FileLoadCompleted(true);
            }
        }        

        public static int CheckUpdatePriority(string priorityValue)
        {
            int count = -1;
            try
            {
                string query = "select count(p.id) " +
                             "from SUVD.SCHEDULED_TODO_ITEMS s, " +
                                   "suvd.projects p, " +
                                   "suvd.contacts c, " +
                                   "suvd.creditor_dogovors d, " +
                                   "suvd.creditors cr " +
                            "where p.id = s.project_id " +
                              "and cr.id = p.creditor_id " +
                              "and c.id = p.debtor_contact_id " +
                              "and d.id = p.dogovor_id " +
                              "and s.project_id in (select p.id " +
                                                     "from IMP_PRIOR t, suvd.projects p " +
                                                    "where p.business_n = t.deal_id) " +
                              "and s.priority_value < " + priorityValue;
                OracleDataReader reader = _con.GetReader(query);
                while (reader.Read())
                {
                    count = Convert.ToInt32(reader[0]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка соединения с базой данных, попробуйте повторить попытку.", "Приоритеты", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PriorityLoger.AddRecordToLog("Ошибка соединения с базой данных. " + ex.Message);
            }
            return count;
        }

        public static void UpdatePriority(string priorityValue)
        {
            InsertIntoReportPriority(priorityValue);
            UpdateScheduledTodoItems(priorityValue);
            AfterCheck(priorityValue);
        }

        private static void AfterCheck(string pv)
        {
            int toUp = 0;
            int done = 0;
            string query = "select count(*) from REPORT.IMP_PRIOR t";
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                toUp = Convert.ToInt32(reader[0]);
            }
            query = "select count(*) " +
                      "from SUVD.SCHEDULED_TODO_ITEMS s, " +
                           "SUVD.PROJECTS p " +
                     "where s.project_id = p.id " +
                       "and p.business_n in (select t.deal_id " +
                                              "from report.IMP_PRIOR t) " +
                       "and s.priority_value = " + pv;
            reader = _con.GetReader(query);
            while (reader.Read())
            {
                done = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            MessageBox.Show("Пинов в файле: " + toUp + ". Из них " + pv + "-ый приоритет сейчас имеют " + done + (toUp == done? "." : (", " + (toUp - done) + " похоже не имеют назначенных задач для поднятия приоритета.")), "Приоритеты");
        }

        private static void UpdateScheduledTodoItems(string priorityValue)
        {
            try
            {
                string query = "update SUVD.SCHEDULED_TODO_ITEMS s " +
                                  "set s.priority_value =  " + priorityValue +
                               " where s.project_id in (select p.id " +
                                 "from report.IMP_PRIOR t, suvd.projects p " +
                                "where p.business_n = t.deal_id) " +
                                (priorityValue == "0"? "" :  "and s.priority_value < " + priorityValue);

                _con.ExecCommand(query);

                if (UpdatePriorityCompleted != null)
                {
                    UpdatePriorityCompleted(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при апдейте таблицы SCHEDULED_TODO_ITEMS, приоритет поднят не был, попробуйте повторить попытку.", "Приоритеты", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PriorityLoger.AddRecordToLog("Возникла ошибка при апдейте таблицы SCHEDULED_TODO_ITEMS, приоритет поднят не был. " + ex.Message);
            }
        }

        private static void InsertIntoReportPriority(string priorityValue)
        {
            try
            {
                string query = "insert into report.priority " +
                                "select p.business_n, " +
                                        "c.inn, " +
                                        "cr.id cred4, " +
                                        "cr.name cred, " +
                                        "d.id reg5, " +
                                        "d.d_number reg, " +
                                        priorityValue + "," +
                                        "trunc(sysdate) " +
                                  "from SUVD.SCHEDULED_TODO_ITEMS s, " +
                                        "suvd.projects p, " +
                                        "suvd.contacts c, " +
                                        "suvd.creditor_dogovors d, " +
                                        "suvd.creditors cr " +
                                  "where p.id = s.project_id " +
                                    "and cr.id = p.creditor_id " +
                                    "and c.id = p.debtor_contact_id " +
                                    "and d.id = p.dogovor_id " +
                                    "and s.project_id in (select p.id " +
                                                           "from report.IMP_PRIOR t, suvd.projects p " +
                                                          "where p.business_n = t.deal_id) " +
                                    "and s.priority_value < " + priorityValue;
                _con.ExecCommand(query);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при записи в таблицу report.priority,  попробуйте повторить попытку.", "Приоритеты", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PriorityLoger.AddRecordToLog("Возникла ошибка при записи в таблицу report.priority. " + ex.Message);
            }
        }

        static public int GetCountUpdatedPins(string priorityValue)
        {
            int count = 0;
            string query = "select count(*) " +
                             "from REPORT.PRIORITY t " +
                            "where trunc(t.dt) = trunc(sysdate) " +
                              "and t.priority = " + priorityValue;
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return count;
        }
       
        /// <summary>
        /// Method for creating xls file with update priority report. 
        /// </summary>
        /// <param name="startDate">Range start date.</param>
        /// <param name="stopDate">Range stop date.</param>
        /// <param name="path">Path to save xls file. Has default value.</param>
        public static void CreateExcelReport(string startDate, string stopDate)
        {
            try
            {
                string file = AppSettings.PathToXls;
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("Priority_report");

                string query = "select t.business_n, t.cred, t.reg, t.dt, t.priority " +
                                 "from REPORT.PRIORITY t " +
                                "where trunc(t.dt) between to_date('" + startDate + "', 'DD/MM/YYYY') and to_date('" + stopDate + "', 'DD/MM/YYYY')";
                OracleDataReader reader = _con.GetReader(query);
                worksheet.Cells[0, 0] = new Cell("Пин");
                worksheet.Cells[0, 1] = new Cell("Кредитор");
                worksheet.Cells[0, 2] = new Cell("Реестр");
                worksheet.Cells[0, 3] = new Cell("Дата");
                worksheet.Cells[0, 4] = new Cell("Приоритет");

                int i = 0;
                while (reader.Read())
                {
                    i += 1;
                    worksheet.Cells[i, 0] = new Cell(reader[0].ToString());
                    worksheet.Cells[i, 1] = new Cell(reader[1].ToString());
                    worksheet.Cells[i, 2] = new Cell(reader[2].ToString());
                    DateTime date = Convert.ToDateTime(reader[3]);
                    worksheet.Cells[i, 3] = new Cell(date.ToString("dd/MM/yyyy"));
                    worksheet.Cells[i, 4] = new Cell(reader[4].ToString());
                }
                reader.Close();                

                if (i == 0)
                {
                    MessageBox.Show("За указанный период не было поднятий приоритетов дел. ", "Приоритеты");                    
                }
                else
                {
                    workbook.Worksheets.Add(worksheet);
                    workbook.Save(file);
                    System.Diagnostics.Process.Start(file);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Похоже файл \"priority.xls\" уже используется. Закройте файл и повторите попытку.", "Ошибка доступа к файлу", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Похоже что-то пошло не так..." + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }
    }
}
