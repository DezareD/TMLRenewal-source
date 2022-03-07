using RenewalTML.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto
{
    public class SystemSettingModel
    {
        public SystemConfiguration configuration { get; set; }
        public InputLoaderState loaderState { get; set; }
    }
}
