using MyLetterManager.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Mediator
{
    public static class Mediator
    {
        static string _currentCreditor = "";
        public static string CurrentCreditor { get { return _currentCreditor; } set { _currentCreditor = value; } } 

        static public decimal CreditorId { get; set; } 

        static public List<Reg> RegList { get; set; }

        public static string DealsCount { get; set; }
    }
}
