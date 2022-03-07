using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto
{
    public class AwardView
    {
        public string Name { get; set; }
        public string HTMLText { get; set; }
        public int ProgressFinal { get; set; }
        public int Progress { get; set; }
        public string HTMLIcon { get; set; }
        public bool isGet { get; set; }
    }
}
