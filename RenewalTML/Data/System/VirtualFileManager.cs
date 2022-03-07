using Microsoft.AspNetCore.Hosting;
using RenewalTML.Data.Model;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

using File = RenewalTML.Data.Model.File;

namespace RenewalTML.Data
{
    public class VirtualFileManager : GenericManager<File>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public static string[] imageMimeTypes = new string[] { "image/png", "image/jpeg" };

        public VirtualFileManager(IRepository<File, int> fileRepository, IWebHostEnvironment hostingEnvironment)
        {
            _genericRepository = fileRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> GetPhysicFileUrl(int id)
        {
            var search = await GetAsync(id);

            if (search != null) return Path.Combine(search.SitePath, search.Name + search.Extension);
            else return null;
        }
    }

    public class VirtualCroopedFileManager : GenericManager<CroppedImageFile>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly VirtualFileManager _fileRepository;
        public static string[] imageMimeTypes = new string[] { "image/png", "image/jpeg" };

        public VirtualCroopedFileManager(IWebHostEnvironment hostingEnvironment, IRepository<CroppedImageFile, int> cropperRepository,
            VirtualFileManager fileRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _genericRepository = cropperRepository;
            _fileRepository = fileRepository;
        }

        public async Task<string> GetPhysicFileCropperUrl(int id)
        {
            var search = await GetAsync(id);

            if (search != null) return await _fileRepository.GetPhysicFileUrl(search.ImageId);
            else return null;
        }
    }
}
