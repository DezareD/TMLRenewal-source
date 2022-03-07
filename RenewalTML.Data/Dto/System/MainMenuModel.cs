using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto
{
    public class MainMenuModel
    {
        public MainMenuModel(List<MenuField> menuFields, string name, bool isHasCollapsed = false, bool isShow = true)
        {
            this.menuFields = menuFields;
            this.isHasCollapsed = isHasCollapsed;
            this.isShow = isShow;
            this.name = name;
        }

        public List<MenuField> menuFields { get; set; }
        public bool isMenuCollapsed { get; set; }
        public bool isHasCollapsed { get; set; }
        public bool isShow { get; set; }
        public string name { get; set; }
    }

    public class MenuField
    {
        public MenuItem item { get; set; }
        public List<MenuItem> childItems { get; set; }
        public bool isOpen { get; set; }
    }

    public class MenuItem
    {
        public MenuItem(string name, string icon = null, string addedContent = null, string url = null, bool isActive = true, bool isFocus = false)
        {
            this.name = name;
            this.icon = icon;
            this.addedContent = addedContent;
            this.url = url;
            this.isActive = isActive;
            this.isFocus = isFocus;
        }

        public string name { get; set; }
        public string icon { get; set; }
        public bool isActive { get; set; }
        public string addedContent { get; set; } // добавоочный контент после текста.
        public string url { get; set; }
        public bool isFocus { get; set; }
    }
}
