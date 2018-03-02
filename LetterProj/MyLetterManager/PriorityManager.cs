using DbLayer;
using MyLetterManager.Repo;
using Oracle.ManagedDataAccess.Client;
using Semaphore.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelLibrary.SpreadSheet;


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

        private static void InsertToDbByBatFile()
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.Exited += new EventHandler(FileLoaded);
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = @"1_IMPORT.BAT";
            proc.EnableRaisingEvents = true;
            proc.Start();
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
                                                     "from LET_APP t, suvd.projects p " +
                                                    "where p.business_n = t.deal_id) " +
                              "and s.priority_value < " + priorityValue;
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return count;
        }

        public static void UpdatePriority(string priorityValue)
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
                                                           "from LET_APP t, suvd.projects p " +
                                                          "where p.business_n = t.deal_id) " +
                                    "and s.priority_value < " + priorityValue;
             _con.ExecCommand(query);

            if (UpdatePriorityCompleted != null)
            {
                UpdatePriorityCompleted(true);
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

        public static void CreateExcelReport(string path = @"d:\priority.xls")
        {
            try
            {
                string file = path;
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("Priority_report");

                string query = "select t.business_n, t.cred, t.reg, t.dt, t.priority " +
                                 "from REPORT.PRIORITY t " +
                                "where trunc(t.dt) = trunc(sysdate)";
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
                    worksheet.Cells[i, 3] = new Cell(reader[3].ToString());
                    worksheet.Cells[i, 4] = new Cell(reader[4].ToString());
                }
                reader.Close();
                workbook.Worksheets.Add(worksheet);
                workbook.Save(file);
                if (i == 0)
                {
                    MessageBox.Show("Сегодня еще не было поднятий приоритетов дел. ", "Приоритеты");                    
                }
                else
                {
                    MessageBox.Show("Готово, путь к файлу отчета: " + path, "Excel отчет");
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Похоже файл уже используется. Закройте файл и повторите попытку.", "Ошибка доступа к файлу", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Похоже что-то пошло не так..." + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }
    }
}
