using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto
{
    public class RequestMoneyModel
    {
        [Range(100, int.MaxValue, ErrorMessage = "Минимальное количество: 100")]
        public int Money { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Text { get; set; }

        [DisableModuleValidation]
        public List<FillTicketImage> FillTicketImage { get; set; }
    }
}
