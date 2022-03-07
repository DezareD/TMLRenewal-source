using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Data.JSInteropHelper
{
    public static class ErrorCatchingFunc
    {
        public static ErrorCatchingInterop isHaveError(string jsonStringify)
        {
            var error = JsonConvert.DeserializeObject<ErrorCatchingInterop>(jsonStringify);

            if (!String.IsNullOrEmpty(error.message))
                error.haveError = true;

            return error;
        }
    }

    public class ErrorCatchingInterop
    {
        public string message { get; set; }
        public string type { get; set; }

        public bool haveError { get; set; }
    }
}
