using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class File : Entity<int>
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; } // path to file C:..../../.../path/to/file/{name}.{extension}
        public double Size { get; set; } // MB
        public string SitePath { get; set; } // [https://localhost/]/path/to/file/{name}.{extenstion}
        public int hourseToDelete { get; set; }

    }
}
