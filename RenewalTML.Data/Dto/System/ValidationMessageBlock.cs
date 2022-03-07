using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto
{
    public class ValidationMessageBlock
    {
        public ValidationMessageBlock(string message, string type)
        {
            this.type = type;
            this.message = message;
            isShow = true;
        }

        public string type { get; set; }
        public string message { get; set; }
        public bool isShow { get; set; }
    }
}
