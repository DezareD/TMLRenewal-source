using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class AdminAction : Entity<int>
    {
        public string Type { get; set; }

        // {to:user:18} - внесены изменения в пользователя конкретного
        // {to:organization:23}
        // {to:system} - внесены изменения в систему ( будь это роли, настройки и т.д )

        public string HtmlText { get; set; }
        public string Date { get; set; }
        public int AdminId { get; set; }
        public string Icon { get; set; }
        public int Priority { get; set; }

        // 0 - не важно, серый цвет
        // 1 - небольшие изменения - зеленый цвет
        // 2 - средние изменения - желтый цвет
        // 3 - системные \ экономические изменения - красный цвет
    }
}
