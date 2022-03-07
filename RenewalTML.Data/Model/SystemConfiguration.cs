using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class SystemConfiguration : Entity<int>
    {
        public string UniqId { get; set; }

        // system_maxBalance пример
        public string Value { get; set; }
        public string Type { get; set; }
        public string Information { get; set; }

        // type_system
        // type_economic

        public string RequeredType { get; set; }

        // bool
        // double
        // int 
        // string
    }
}
