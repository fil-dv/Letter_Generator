using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Repo
{
    public static class DataToGenerate
    {
        static decimal _templateId = -1;
        static public decimal TemplateId { get { return _templateId; } set { _templateId = value; } }
        static List<Deal> _dealList = new List<Deal>();
        static public List<Deal> DealList { get { return _dealList; } set { _dealList = value; } }
        static List<Reg> _regList = new List<Reg>();
        static public List<Reg> RegList { get { return _regList; } set { _regList = value; } }

        static public void Reset()
        {
            _templateId = -1;
            _dealList = new List<Deal>();
            _regList = new List<Reg>();
        }

    }
}
