using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Repo
{
    public static class QueryBuilder
    {
        public static void HeaderScriptBuilder(ref string query, string command)
        {
            switch (command)
            {
                case "select by reg":
                    query = "select p.business_n " +
                              "from suvd.projects p, suvd.contact_address a, suvd.contacts c " +
                             "where p.dogovor_id in (" + LetterManager.GetRegStr() + ") " +
                               "and a.contact_id = p.debtor_contact_id " +
                               "and c.id = p.debtor_contact_id " +
                               "and a.role = " + LetterManager.WriteAdressType();
                    break;

                case "insert":
                    break;
            }
        }
    }
}
