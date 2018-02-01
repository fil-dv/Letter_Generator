using DbLayer;
using MyLetterManager.Repo;
using Oracle.ManagedDataAccess.Client;
using Semaphore.Infrastructure.Settings;
using System;
using System.Collections.Generic;
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
        static public List<Deal> _dealList = new List<Deal>();
        static public List<Reg> _listRegToGenerate = new List<Reg>();
        static public List<Deal> _listDealsToGenerate = new List<Deal>();
        static public List<Condition> _listConditions = new List<Condition>();
        static public string _adress_type = "";

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
                string str = item.Alias.Replace('«', '"');
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

        static public void ResetData()
        {
            _creditorList.Clear();
            _creditorRegsList.Clear();
            _dealList.Clear();
            _listRegToGenerate.Clear();
            _listDealsToGenerate.Clear();
            _listConditions.Clear();
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

        static public void AddRegForGenerate(decimal regId)
        {
            List<Reg> list = _creditorRegsList.Where(r => r.Id == regId).ToList();
            if (list.Count > 0)
            {
                _listRegToGenerate.Add(list[0]);
            }            
        }

        static public string QueryBuilder(string command)
        {
            string query = "";
            HeaderScriptBuilder(ref query, command);
            ConditionsScriptBuilder(ref query);
            return query;
        }

        static void HeaderScriptBuilder(ref string query, string command)
        {
            switch (command)
            {
                case "select":
                    query = "select p.business_n " +
                              "from suvd.projects p, suvd.contact_address a, suvd.contacts c " +
                             "where p.dogovor_id in (" + GetRegStr() + ") " +
                               "and a.contact_id = p.debtor_contact_id " +
                               "and c.id = p.debtor_contact_id " +
                               "and a.role = " + WriteAdressType();
                    break;

                case "insert":
                    break;
            }
        }

        static public void SetAdressType(string adrType)
        {
            _adress_type = adrType;
        }

        static string WriteAdressType()
        {
            switch (_adress_type)
            {
                case "Прописка":
                    return "1";
                    break;
                case "Фактический":
                    return "3";
                    break;
                case "Временной регистрации":
                    return "2";
                    break;
                case "Рабочий":
                    return "4";
                    break;
                default:
                    return "1";                   
            }
        }

        static string GetRegStr()
        {
            string str = "";
            if (_listRegToGenerate.Count > 0)
            {
                foreach (var item in _listRegToGenerate)
                {
                    str += (item.Id.ToString() + ",");
                }
                str = str.Remove(str.Count() -1, 1);
            }
            return str;
        }

        static void ConditionsScriptBuilder(ref string query)
        {

        }

        static public int CheckDealsCountToGenerate()
        {
            int count = 0;
            string query = QueryBuilder("select");
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                count++;
            }            
            reader.Close();
            return count;
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
                if (Convert.ToInt32(reader[3]) == 0) con.IsUsed = false;
                else con.IsUsed = true;
                _listConditions.Add(con);
            }
            reader.Close();
            return _listConditions;
        }

        static public void ChangeConditionUsing(string conditionId, bool isUsed)
        {
            foreach (var item in _listConditions)
            {
                if (item.Id == Convert.ToDecimal(conditionId)) item.IsUsed = isUsed;
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

    }
}
