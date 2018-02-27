using DbLayer;
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
       
        static public List<Creditor> _creditorList = new List<Creditor>();
        static public List<Reg> _creditorRegsList = new List<Reg>();
        static public List<LetterTemplate> _templateList = new List<LetterTemplate>();
        static public List<Condition> _listConditions = new List<Condition>();

        static public void ResetData()
        {
            _creditorList.Clear();
            _creditorRegsList.Clear();
         //   _dealList.Clear();
            _templateList.Clear();
            _listConditions.Clear();
            DataToGenerate.Reset();
            // _adress_type = "";
            TruncateTable();
        }

        private static void TruncateTable()
        {
            string query = "truncate table let_app";
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

                string query = "select distinct cr.id, cr.name from suvd.creditors cr, suvd.creditor_dogovors cd where cr.id = cd.creditor_id  and trunc(cd.stop_date) > trunc(sysdate)";
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

                string query = "select distinct t.id, t.name from suvd.templates t";
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

                string query = "select cd.id, cd.d_number from suvd.creditor_dogovors cd where cd.creditor_id = " + id + " and trunc(cd.stop_date) > trunc(sysdate) ";
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
            File.WriteAllText("Imp.csv", String.Empty);

            if (DataToGenerate.RegList.Count > 0)
            {
                string query = "SELECT t.business_n FROM SUVD.PROJECTS t WHERE t.dogovor_id in ( " + GetRegsStr() + ")";
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

        static public int GetPinCountToGenerate()
        {
            int count = 0;
            string query = "";
            if (_listConditions.Count == 0)
            {
                query = "select count(*) from LET_APP";
            }
            else
            {
                query = "SELECT count(*) FROM let_app WHERE ";
                AddConditions(ref query);
            }
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return count;
        }

        private static void AddConditions(ref string query)
        {
            List<Condition> checkedList = _listConditions.Where(c => c.IsChecked == true).ToList();
            if (checkedList.Count > 0)
            {
                foreach (var item in checkedList)
                {
                    query += (item.Script + " AND ");
                }
            }
        }

        static public List<Condition> GetConditionsList()
        {
            _listConditions.Clear();
            string query = "select t.id, t.description, t.script, t.is_used from LETTERS_CONDITIONS t";
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                Condition con = new Condition();
                con.Id = Convert.ToDecimal(reader[0]);
                con.Text = reader[1].ToString();
                con.Script = reader[2].ToString();                
                if (Convert.ToInt32(reader[3]) == 0) con.IsChecked = false;
                else con.IsChecked = true;
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
            string query = "select count(*) from SUVD.LETTER_QUEUE t where t.status = 0";
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
            string query = "select t.name, length(t.name) from SUVD.TEMPLATES t where t.id = " + templateId;
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
                            break;
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
            //UpdateAddAdress();
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
            string query = "UPDATE LET_APP t " +
                              "SET t.adr = " +
                          "(SELECT ca.zip_code || ', ' || ca.region || ', ' || ca.city || ', ' || ca.street " +
                             "FROM SUVD.PROJECTS p, suvd.contact_address ca " +
                            "WHERE t.deal_id = p.business_n " +
                              "AND t.adr_type = ca.role " +
                              "AND p.debtor_contact_id = ca.contact_id " +
                              "AND ca.zip_code is not null)";
            _con.ExecCommand(query);
            query = "delete from LET_APP t WHERE t.adr is null";
            _con.ExecCommand(query);
        }

        static public event Action<bool> FileLoadCompleted;

    }
}
