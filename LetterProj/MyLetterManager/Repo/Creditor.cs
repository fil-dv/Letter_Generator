using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Repo
{
    public class Creditor
    {
        string Name { get; set; }
        string Id { get; set; }

        List<Reg> _regList = new List<Reg>();
        public List<Reg> DealList { get { return _regList; } set { _regList = value; } }
    }
}
