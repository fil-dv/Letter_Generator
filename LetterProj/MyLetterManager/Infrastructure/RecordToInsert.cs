using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Repo
{
    public enum AdressType { ap, af, avr, p_f, p_avr, p_f_avr, work }

    public enum Operation { Insert, Remove }

    public enum Resource { Regs, Pins }

    public class RecordToInsert
    {
        public Reg Reestr { get; set; }
        public string DealId { get; set; }
        public string TemplateId { get; set; }
        public AdressType AdrType { get; set; }
    }
}
