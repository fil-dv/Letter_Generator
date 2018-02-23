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
    public static class PriorityManager
    {
        static OracleConnect _con;

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

        public static int CheckUpdatePriority(List<Deal> pinList, string priorityValue)
        {
            int count = -1;

            string query = "select count(*) " +
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
            OracleDataReader reader = _con.GetReader(query);
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[1]);
            }
            reader.Close();
            return count;
        }
    }
}
