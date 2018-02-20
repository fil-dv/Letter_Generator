using DbLayer;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Repo
{
    

    public static class QueryBuilder
    {
        public static void ScriptBuilder(OracleConnect connnect, ref string query, string command)
        {
            //if (DataToGenerate.RegList.Count == 0 && DataToGenerate.DealList.Count == 0)
            //{
            //    return; 
            //}

            switch (command)
            {
                //case "select by reg":
                //    query = "select p.business_n " +
                //              "from suvd.projects p, suvd.contact_address a, suvd.contacts c " +
                //             "where p.dogovor_id in (" + LetterManager.GetRegStr() + ") " +
                //               "and a.contact_id = p.debtor_contact_id " +
                //               "and c.id = p.debtor_contact_id " +
                //               "and a.role = " + LetterManager.WriteAdressType();
                //    break;

                case "check_count":



                    //query = DbReader(connnect, command) + " ";
                    //string regs = DataToGenerate.RegList.Count > 0? LetterManager.GetRegStr() : "";
                    //string pins = DataToGenerate.DealList.Count > 0? LetterManager.GetPinStr() : "";
                    //query += (" a.contact_id = p.debtor_contact_id and c.id = p.debtor_contact_id ");
                    //query += ("and a.role = " + LetterManager.WriteAdressType());
                    //if(regs.Length > 0)
                    //{
                    //    query += (" and p.dogovor_id in (" + regs + ") ");
                    //}
                    //if(pins.Length > 0)
                    //{
                    //    query += (" and p.business_n in (" + pins + ") ");
                    //}


                    break;

                case "insert":
                    break;
            }
        }

        static string DbReader(OracleConnect connnect, string queryName)
        {
            string str = "";
            string query = "select t.query_block from LETTER_BLDR t where t.name = '" + queryName + "'";
            OracleDataReader reader = connnect.GetReader(query);
            while (reader.Read())
            {
                str = reader[0].ToString();                
            }
            reader.Close();
            return str;
        }

        static void ConditionsScriptBuilder(ref string query)
        {

        }
    }
}
