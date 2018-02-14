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

        public static string DealsCount { get; set; }
    }
}
