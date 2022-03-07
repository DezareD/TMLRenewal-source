using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class CroppedImageFile: Entity<int>
    {
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public int ImageId { get; set; }
    }
}
