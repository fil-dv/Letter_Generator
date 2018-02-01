using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Repo
{
    public class Condition
    {
        public decimal Id { get; set; }
        public string Text { get; set; }
        public string Script { get; set; }
        public bool IsUsed { get; set; }
    }
}
