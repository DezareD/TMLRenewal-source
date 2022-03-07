using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto
{
    public class SendMoneyUserToUser
    {
        [MinLength(3, ErrorMessage = "Минимальная длина поля: 3 символа")]
        public string UserName { get; set; }

        [Range(100, int.MaxValue, ErrorMessage = "Минимальное количество: 100")]
        public int Money { get; set; }

        [DisableModuleValidation]
        public bool AddTexes { get; set; }

        [DisableModuleValidation]
        public int SpendMoney { get; set; }

        [DisableModuleValidation]
        public int TransMoney { get; set; }

        [DisableModuleValidation]
        public double Tax { get; set; }
    }
}
