using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Repo
{
    public enum AdressType { ap, af, avr, p_f, p_avr, p_f_avr, work }

    public class RecordToInsert
    {
        public string DealId { get; set; }
        public string TemplateId { get; set; }
        public AdressType AdrType { get; set; }
    }
}
