using RenewalTML.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto
{
    public class AdminActionModel
    {
        public AdminAction action { get; set; }
        public Client admin { get; set; }
    }
}
