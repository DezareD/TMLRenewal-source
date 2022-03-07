using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Data.JSInteropHelper
{
    public class ChartDataIntrop
    {
        public ChartDataIntrop(string label, int data)
        {
            Label = label;
            Data = data;
        }

        public string Label { get; set; }
        public int Data { get; set; }
    }
}
