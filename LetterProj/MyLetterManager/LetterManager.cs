using DbLayer;
using ExcelLibrary.SpreadSheet;
using MyLetterManager.Repo;
using Oracle.ManagedDataAccess.Client;
using Semaphore.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyLetterManager
{
    public static class LetterManager
    {
        static OracleConnect _con;

        static public event Action<bool> FileLoadCompleted;

        static public List<Creditor> _creditorList = new List<Creditor>();
        static public List<Reg> _creditorRegsList = new List<Reg>();
        static public List<LetterTemplate> _templateList = new List<LetterTemplate>();
        static public List<Condition> _listConditions = new List<Condition>();

        static string GetQuery(int id)
        {
            string res = "empty";
            try
            {
                string query = "select t.query from report.let_queries t where t.id = " + id;
                OracleDataReader reader = _con.GetReader(query);
                while (reader.Read())
                {
                    res = reader[0].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from MyLetterManager.LetterManager.GetQuery()" + ex.Message); 
            }
            return res;
        }

        static public void ResetData()
        {
            _creditorList.Clear();
            _creditorRegsList.Clear();
            _templateList.Clear();
            _listConditions.Clear();
            DataToGenerate.Reset();
            TruncateTable();
        }

        private static void TruncateTable()
        {
            string query = GetQuery(3); // "truncate table let_app";
            _con.ExecCommand(query);
        }

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
                MessageBox.Show("Exception from MyLetterManager.LetterManager.ExecCommand()" + ex.Message);
            }
        }

        static void GetListWithAliases(ref List<Creditor> inputList)
        {
            foreach (var item in inputList)
            {
                item.Alias = item.Name;
                string str = item.Alias.Replace('«', '"').ToUpper();
                str = str.Replace('»', '"');
                str = str.Replace("БАНК", "");
                item.Name = str;
                item.Alias = GetAlias(item.Name);
            }
        }

        static string GetAlias(string name)
        {
            string alias = "";
            string[] arr = name.Split('"');
            if (arr.Count() > 1)
            {
                alias = arr[arr.Count() - 2];
            }            
            return alias;
        }

        public static List<Creditor> GetCreditorList()
        {            
            try
            {
                 _creditorList.Clear();

                string query = GetQuery(1); // "select distinct cr.id, cr.name from suvd.creditors cr, suvd.creditor_dogovors cd where cr.id = cd.creditor_id  and (trunc(cd.stop_date) > trunc(sysdate) or cd.stop_date is null)";
                OracleDataReader reader = _con.GetReader(query);
                while (reader.Read())
                {
                    Creditor cr = new Creditor();
                    cr.Id = Convert.ToDecimal(reader[0]);
                    cr.Name = reader[1].ToString(); 
                    _creditorList.Add(cr);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from MyLetterManager.LetterManager.GetCreditorList(). " + ex.Message);
            }
            GetListWithAliases(ref _creditorList);
            return _creditorList;
        }

        public static List<LetterTemplate> GetTemplateList()
        {
            try
            {
                _templateList.Clear();

                string query = GetQuery(4);  // "select distinct t.id, t.name from suvd.templates t";
                OracleDataReader reader = _con.GetReader(query);
                while (reader.Read())
                {
                    LetterTemplate lt = new LetterTemplate();
                    lt.Id = Convert.ToDecimal(reader[0]);
                    lt.Name = reader[1].ToString();
                    _templateList.Add(lt);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from MyLetterManager.LetterManager.GetTemplateList(). " + ex.Message);
            }

            return _templateList;
        }

        public static decimal GetCreditorIdByTrimedAlias(string name)
        {
            decimal id = -1;
            if (_creditorList.Count > 0)
            {
                List<Creditor> list = _creditorList.Where(x => x.Alias.Trim() == name).ToList();
                if (list.Count() > 0)
                {
                    id = list[0].Id;
                }
            }
            return id;
        }

        static public List<Reg> GetRegListByCreditorId(decimal id)
        {
            try
            {
                _creditorRegsList.Clear();

                string query = GetQuery(2) + id + GetQuery(21); // "select cd.id, cd.d_number from suvd.creditor_dogovors cd where cd.creditor_id = " + id + " and (trunc(cd.stop_date) > trunc(sysdate) or cd.stop_date is null)";
                OracleDataReader reader = _con.GetReader(query);
                while (reader.Read())
                {
                    Reg reg = new Reg();
                    reg.Id = Convert.ToDecimal(reader[0]);
                    reg.Name = reader[1].ToString();
                    _creditorRegsList.Add(reg);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from MyLetterManager.LetterManager.GetRegListByCreditorId(). " + ex.Message);
            }
            return _creditorRegsList;
        }        

        static public bool IsRegExist(decimal id)
        {
            bool isExist = false;
            List<Reg> list = _creditorRegsList.Where(r => r.Id == id).ToList();
            if (list.Count > 0)
            {
                isExist = true;
            }
            return isExist;
        }

        #region Add or remove pins by reg numbers

        static public void ChangeRegForGenerate(RecordToInsert record, Operation operation)
        {
            List<Reg> list = _creditorRegsList.Where(r => r.Id == record.Reestr.Id).ToList();
            if (list.Count > 0)
            {
                if (operation == Operation.Insert)
                {
                    DataToGenerate.RegList.Add(list[0]);
                }
                else
                {
                    DataToGenerate.RegList.Remove(list[0]);
                }
                WritePinsToFile(record);
            }
            else
            {
                MessageBox.Show("Некорректный номер реестра.");
            }                   
        }

        private static void WritePinsToFile(RecordToInsert record)
        {
            SetCtlFile();
            File.WriteAllText("Imp.csv", String.Empty);

            if (DataToGenerate.RegList.Count > 0)
            {
                string query = GetQuery(14) + GetRegsStr() + ")";
                // "SELECT t.business_n FROM SUVD.PROJECTS t WHERE t.dogovor_id in ( " + GetRegsStr() + ")";
                OracleDataReader reader = _con.GetReader(query);
                List<RecordToInsert> recList = new List<RecordToInsert>();
                while (reader.Read())
                {
                    RecordToInsert rec = new RecordToInsert { DealId = reader[0].ToString(), TemplateId = record.TemplateId, AdrType = record.AdrType };
                    recList.Add(rec);
                }
                reader.Close();
                AddPinFromFile(recList);
            }
            else
            {
                InsertToDbByBatFile();
            }
        }        

        public static string GetRegsStr()
        {
            string str = "";
            if (DataToGenerate.RegList.Count > 0)
            {
                foreach (var item in DataToGenerate.RegList)
                {
                    str += (item.Id.ToString() + ",");
                }
                str = str.Remove(str.Count() - 1, 1);
            }
            return str;
        }
        #endregion        

        static public int GetPinCountToGenerate(string minSum)
        {
            int count = 0;
            string query = "";
            List<Condition> checkedList = _listConditions.Where(c => c.IsChecked == true).ToList();
            if (checkedList.Count == 0)
            {
                query = GetQuery(5);  // "select count(*) from LET_APP";
            }
            else
            {
                query = GetQuery(6);
                        //"SELECT count(*) " + 
                        //  "FROM let_app l, suvd.projects p, suvd.contact_address a, suvd.contacts c " +
                        // "WHERE l.deal_id = p.business_n " + 
                        //   "AND a.contact_id = p.debtor_contact_id " +
                        //   "AND c.id = p.debtor_contact_id " +
                        //   "AND a.role = l.adr_type";
                AddConditionsToQuery(ref query, minSum);
            }
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return count;
        }

        private static void AddConditionsToQuery(ref string query, string minSum)
        {
            List<Condition> checkedList = _listConditions.Where(c => c.IsChecked == true).ToList();
            if (checkedList.Count > 0)
            {
                foreach (var item in checkedList)
                {                    
                    query += (" AND " + item.Script);
                    if (item.Text == "Сумма долга не мение (грн)")
                    {
                        query += (" " + minSum);
                    }
                }
                //query = query.Substring(0, query.LastIndexOf("AND"));
            }
        }

        static public List<Condition> GetConditionsList()
        {
            _listConditions.Clear();
            string query = GetQuery(7);  // "select t.id, t.description, t.script, t.is_used, t.alternative from LETTERS_CONDITIONS t";
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                Condition con = new Condition();
                con.Id = Convert.ToDecimal(reader[0]);
                con.Text = reader[1].ToString();
                con.Script = reader[2].ToString();  
                if (Convert.ToInt32(reader[3]) == 0) con.IsChecked = false;
                else con.IsChecked = true;
                con.Alternative = reader[4].ToString();
                _listConditions.Add(con);
            }
            reader.Close();
            return _listConditions;
        }

        static public void ChangeConditionUsing(string conditionId, bool isChecked)
        {
            foreach (var item in _listConditions)
            {
                if (item.Id == Convert.ToDecimal(conditionId)) item.IsChecked = isChecked;
            }           
        }

        static public int GetCountInQueueToGeneration()
        {
            int count = -1;
            string query = GetQuery(8); // "select count(*) from SUVD.LETTER_QUEUE t where t.status = 0";
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {                
                count = Convert.ToInt32(reader[0]);             
            }
            reader.Close();
            return count;
        }

        static public string GetTemplateName(string templateId)
        {
            string templateName = "";
            string query = GetQuery(9) + templateId;
            // "select t.name, length(t.name) from SUVD.TEMPLATES t where t.id = " + templateId;
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                templateName = reader[0].ToString();
            }
            reader.Close();
            return templateName;
        }

        static public bool IsTemplateExists(decimal templateId, ref string templateName)
        {
            bool isExist = false;
            List<LetterTemplate> list = _templateList.Where(t => t.Id == templateId).ToList();
            if (list.Count > 0)
            {
                isExist = true;
                templateName = list[0].Name;
            }
            return isExist;
        }

        static public void AddPinFromFile(List<RecordToInsert> insertList)
        {
            using (FileStream fs = new FileStream(@"Imp.csv", FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (var item in insertList)
                {
                    switch (item.AdrType)
                    {
                        case AdressType.ap:
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";1" );
                            break;
                        case AdressType.af:
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";3");
                            break;
                        case AdressType.avr:
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";2");
                            break;
                        case AdressType.work:
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";4");
                            break;
                        case AdressType.p_f:
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";1");
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";3");
                            break;
                        case AdressType.p_avr:
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";1");
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";2");
                            break;
                        case AdressType.p_f_avr:
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";1");
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";2");
                            sw.WriteLine(item.DealId + ";" + item.TemplateId + ";3");
                            break;
                        default:
                            MessageBox.Show("Некорректный тип адреса.");
                            return;
                    }
                }
            }
            InsertToDbByBatFile();                       
        }

        private static void InsertToDbByBatFile()
        {            
            Process proc = new Process();
            proc.Exited += new EventHandler(FileLoaded);
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = @"1_IMPORT.BAT";
            proc.EnableRaisingEvents = true;
            proc.Start();
        }

        static void FileLoaded(object sender, EventArgs e)
        {
            UpdateAddAdress();

            if (FileLoadCompleted != null)
            {
                FileLoadCompleted(true);
            }
        }

        private static void UpdateAddAdress()
        {
            string query = GetQuery(10);
                          // "UPDATE LET_APP t " +
                          //    "SET t.adr = " +
                          //"(SELECT ca.zip_code || ', ' || ca.region || ', ' || ca.city || ', ' || ca.street " +
                          //   "FROM SUVD.PROJECTS p, suvd.contact_address ca " +
                          //  "WHERE t.deal_id = p.business_n " +
                          //    "AND t.adr_type = ca.role " +
                          //    "AND p.debtor_contact_id = ca.contact_id " +
                          //    "AND ca.zip_code is not null)";
            _con.ExecCommand(query);
          //  query = "delete from LET_APP t WHERE t.adr is null";
          //  _con.ExecCommand(query);
        }
        
        static public void CreateExcelReport(string minSum, string path = @"leters.xls")
        {
            try
            {
                string file = path;
                Workbook workbook = new Workbook();
                Worksheet worksheet_plus = new Worksheet("+");
                string query = GetQuery(11);
                               //"SELECT p.business_n, decode(l.adr_type, 1, 'АП', 2, 'АВР', 3, 'АФ', 4, 'Рабочий') adr " +
                               //  "FROM let_app l, suvd.projects p, suvd.contact_address a, suvd.contacts c " +
                               // "WHERE l.deal_id = p.business_n " +
                               //   "AND a.contact_id = p.debtor_contact_id " +
                               //   "AND c.id = p.debtor_contact_id " +
                               //   "AND a.role = l.adr_type";
                AddConditionsToQuery(ref query, minSum);
                OracleDataReader reader = _con.GetReader(query);
                worksheet_plus.Cells[0, 0] = new Cell("Пин");
                worksheet_plus.Cells[0, 1] = new Cell("Тип адреса");
                int i = 0;
                while (reader.Read())
                {
                    i += 1;
                    worksheet_plus.Cells[i, 0] = new Cell(reader[0].ToString());
                    worksheet_plus.Cells[i, 1] = new Cell(reader[1].ToString());
                }
                reader.Close();
                workbook.Worksheets.Add(worksheet_plus);

                Worksheet worksheet_minus = new Worksheet("-");
                worksheet_minus.Cells[0, 0] = new Cell("Пин");
                worksheet_minus.Cells[0, 1] = new Cell("Тип адреса");
                worksheet_minus.Cells[0, 2] = new Cell("Причина");
                
                List<Condition> checkedList = _listConditions.Where(c => c.IsChecked == true).ToList();
                if (checkedList.Count > 0)
                {
                    i = 0;
                    foreach (var item in checkedList)
                    {
                        query = GetQuery(12);
                                //"SELECT p.business_n, decode(l.adr_type, 1, 'АП', 2, 'АВР', 3, 'АФ', 4, 'Рабочий') adr " +
                                //  "FROM let_app l, suvd.projects p, suvd.contact_address a, suvd.contacts c " +
                                // "WHERE l.deal_id = p.business_n " +
                                //   "AND a.contact_id = p.debtor_contact_id " +
                                //   "AND c.id = p.debtor_contact_id " +
                                //   "AND a.role = l.adr_type ";
                        query += (" AND " + item.Alternative);
                        if (item.Text == "Сумма долга не мение (грн)")
                        {
                            query += " " + minSum;
                        }
                        reader = _con.GetReader(query);
                        while (reader.Read())
                        {
                            i += 1;
                            worksheet_minus.Cells[i, 0] = new Cell(reader[0].ToString());
                            worksheet_minus.Cells[i, 1] = new Cell(reader[1].ToString());
                            worksheet_minus.Cells[i, 2] = new Cell(item.Text);                            

                            if (item.Text == "Сумма долга не мение (грн)")
                            {
                                worksheet_minus.Cells[i, 2] = new Cell("Сумма долга мение " + minSum + " грн.");
                            }
                        }
                        reader.Close();
                    }
                    workbook.Worksheets.Add(worksheet_minus);
                    workbook.Save(file);
                    System.Diagnostics.Process.Start(file);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Похоже файл \"leters.xls\" уже используется. Закройте файл и повторите попытку.", "Ошибка доступа к файлу", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Похоже что-то пошло не так..." + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static public bool IsExistPinsInTable()
        {
            bool res = false;
            try
            {
                string query = GetQuery(13);  // "select count(*) from let_app t";
                OracleDataReader reader = _con.GetReader(query);
                int count = 0;
                while (reader.Read())
                {
                    count = Convert.ToInt32(reader[0]);
                }
                reader.Close();
                if (count > 0) res = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception from MyLetterManager.LetterManager.IsExistPinsInWork(). " + ex.Message);
            }
            return res;
        }

        private static void SetCtlFile()
        {
            string text = "LOAD DATA " + Environment.NewLine +
                          "INFILE 'imp.csv' " + Environment.NewLine +
                          "REPLACE " + Environment.NewLine +
                          "INTO TABLE \"LET_APP\" " + Environment.NewLine +
                          "FIELDS TERMINATED BY ';' " + Environment.NewLine +
                          "TRAILING NULLCOLS " + Environment.NewLine +
                          "( " + Environment.NewLine +
                          "deal_id, " + Environment.NewLine +
                          "template, " + Environment.NewLine +
                          "adr_type, " + Environment.NewLine +
                          "comment1, " + Environment.NewLine +
                          "comment2, " + Environment.NewLine +
                          "comment3, " + Environment.NewLine +
                          "adr, " + Environment.NewLine +
                          ")";
            File.WriteAllText("1_import.CTL", text);
        }
    }
}
