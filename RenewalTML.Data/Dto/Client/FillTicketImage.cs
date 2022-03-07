using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalTML.Data.Dto
{
    public class FillTicketImage
    {
        public string ImageUrl { get; set; }
        public int ImageId { get; set; }

        public bool isFilled { get; set; }
        public bool isUploadBlock { get; set; }
        public bool isLoading { get; set; }
    }
}
