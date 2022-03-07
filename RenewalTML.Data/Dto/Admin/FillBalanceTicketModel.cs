using RenewalTML.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto.Admin
{
    public class AbastractTicketModelOverride
    {
        public Ticket _ticket { get; set; }
    }

    public class FillBalanceTicketModel : AbastractTicketModelOverride
    {
        public string TextInformation { get; set; }
        public int CountMoney { get; set; }
        public List<int> ImagesIds { get; set; }
    }
}
