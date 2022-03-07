using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Data.JSInteropHelper
{
    public class VKAuthorizeDataInterop
    {
        public VKAuthorizeSessionObject session { get; set; }
        public string status { get; set; }
    }

    public class VKAuthorizeSessionObject
    {
        public int expire { get; set; }
        public int mid { get; set; }
        public string secret { get; set; }
        public string sid { get; set; }
        public string sig { get; set; }

        public VKAuthorizeUserObject user { get; set; }

    }

    public class VKAuthorizeUserObject
    {
        public string domain { get; set; }
        public string first_name { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string last_name { get; set; }
        public string nickname { get; set; }
    }
}
