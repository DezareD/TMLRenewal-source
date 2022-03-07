using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto.Admin
{
    public class AdminRequestTicketModel
    {
        [Range(100, int.MaxValue, ErrorMessage = "Минимальное количество: 100")]
        public int Money { get; set; }

        [DisableModuleValidation]
        public string Text { get; set; }

        [DisableModuleValidation]
        public List<FillTicketImage> FillTicketImages { get; set; }
        public string AdminText { get; set; }

        [Required(ErrorMessage = "Выбор обязателен")]
        public string AdminApproval { get; set; }
    }
}
