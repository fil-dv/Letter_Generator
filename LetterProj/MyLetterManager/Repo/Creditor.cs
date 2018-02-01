using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Repo
{
    public class Creditor
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public decimal Id { get; set; }

        List<Reg> _regList = new List<Reg>();
        public List<Reg> DealList { get { return _regList; } set { _regList = value; } }
    }
}
