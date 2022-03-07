using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto
{
    public class TooltipModel
    {
        public TooltipModel(string name, string html, ToolTipStyles styles, bool interactive = false, int width = 350)
        {
            ElementName = name;
            InnerHtmlText = html;

            isInteractive = interactive;
            Width = width;

            if (styles == ToolTipStyles.DefaultStyle) toolTipStyles = "-tml-tippytheme-main";
            else if (styles == ToolTipStyles.SecondaryStyle) toolTipStyles = "-tml-tippytheme-secondary";
        }

        public string ElementName { get; private set; }
        public string InnerHtmlText { get; private set; }
        public string toolTipStyles { get; private set; }
        public bool isInteractive { get; private set; }
        public int Width { get; private set; }
    }

    public enum ToolTipStyles
    {
        DefaultStyle,
        SecondaryStyle
    }
}
