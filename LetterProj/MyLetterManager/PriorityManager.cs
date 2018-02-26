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

        static public event Action<bool> FileLoadCompleted;

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
            int res = _con.ExecCommand(query);
            if (res == 1)
            {
                MessageBox.Show("Готово.");
            }
        }
    }
}
