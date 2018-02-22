using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Repo
{
    public class Reg
    {
        public string Name { get; set; }
        public decimal Id { get; set; }

        List<Deal> _dealList = new List<Deal>();
        public List<Deal>  DealList { get{ return _dealList; } set { _dealList = value; } }
         
    }
}
