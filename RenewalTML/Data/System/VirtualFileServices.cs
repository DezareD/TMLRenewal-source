using Blazorise;
using Microsoft.AspNetCore.Hosting;
using RenewalTML.Data.Dto;
using RenewalTML.Data.JSInteropHelper;
using RenewalTML.Data.Model;
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Webp;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Validation;
using File = RenewalTML.Data.Model.File;
using Image = SixLabors.ImageSharp.Image;

namespace RenewalTML.Data
{
    public class VirtualUserFileStatic : VirtualFileStatic
    {
        public static string defaultAvatarName = "_img_defaultavatar";
        public static string defaultAvatarNamex64 = "x64__img_defaultavatar";
        public static string x64imagePrefix = "x64_";

        public VirtualUserFileStatic(IWebHostEnvironment hostingEnvironment, IVirtualFileServices fileServices) : base(hostingEnvironment, fileServices) { }

        public async Task RecroppedImage(int croppedDataId, int mainImageId, CroppedImageInfo newInfo, int quality = 70)
        {
            var dataCropped = await fileServices.GetCroppedImageFile(croppedDataId);

            var mainFile = await fileServices.GetFileById(mainImageId);
            var fileCroppedImage = await fileServices.GetFileById(dataCropped.ImageId);

            var filePathMain = Path.Combine(mainFile.Path, mainFile.Name + mainFile.Extension);
            var filePathCropped = Path.Combine(fileCroppedImage.Path, fileCroppedImage.Name + fileCroppedImage.Extension);

            var filePathCroppedx64 = Path.Combine(fileCroppedImage.Path, "x64_" + fileCroppedImage.Name + fileCroppedImage.Extension);

            if (mainFile != null)
            {
                try
                {
                    System.IO.File.Delete(filePathCropped); // удаляем старый файл
                    System.IO.File.Delete(filePathCroppedx64);
                }
                catch (Exception) { }

                using (var instream = new FileStream(filePathMain, FileMode.Open)) // перезаписываем на новый
                {
                    instream.Seek(0, SeekOrigin.Begin);

                    using (var image = await Image.LoadAsync(instream))
                    {
                        image.Mutate(i => i.Crop(Rectangle.Round(new RectangleF((float)newInfo.X, (float)newInfo.Y, (float)newInfo.Width, (float)newInfo.Height))));

                        await image.SaveAsWebpAsync(filePathCropped, encoder: new WebpEncoder()
                        {
                            Quality = quality
                        });
                    }
                }

                using (var instream = new FileStream(filePathMain, FileMode.Open)) // перезаписываем на новый
                {
                    instream.Seek(0, SeekOrigin.Begin);

                    using (var image = await Image.LoadAsync(instream))
                    {
                        image.Mutate(i => i.Crop(Rectangle.Round(new RectangleF((float)newInfo.X, (float)newInfo.Y, (float)newInfo.Width, (float)newInfo.Height))));

                        await image.SaveAsWebpAsync(filePathCroppedx64, encoder: new WebpEncoder()
                        {
                            Quality = quality
                        });
                    }
                }

                // Изменяем только главный кроп файл
                dataCropped.x = newInfo.X;
                dataCropped.y = newInfo.Y;
                dataCropped.height = newInfo.Height;
                dataCropped.width = newInfo.Width;
                await fileServices.UpdateFileCrooped(dataCropped);
            }
        }

        public async Task<UserImageInfo> CreateCopyDefaultCropperAvatarToUser(int quality = 100, string imagePrefix = "_img_")
        {
            var getDefaultCroppImage = await fileServices.GetCroppedImageFile(1);
            var getDefaultCroppImagex64 = await fileServices.GetCroppedImageFile(2);

            var filehmain = await fileServices.GetFileById(getDefaultCroppImage.ImageId);
            var filex64 = await fileServices.GetFileById(getDefaultCroppImagex64.ImageId);

            var level1 = ClientAuthServices.GenerateRandomString(2, false).ToUpperInvariant();
            var level2 = ClientAuthServices.GenerateRandomString(2, false).ToUpperInvariant();

            var pathSite = "/imgs/" + "userImages/" + level1 + "/" + level2 + "/";

            var pathAbsolute = Path.Combine(hostingEnvironment.WebRootPath, "imgs", "userImages", level1, level2);

            var filename = imagePrefix + ClientAuthServices.GenerateRandomString(15, false);

            var path = Path.Combine(pathAbsolute, filename + ".webp");

            var pathx64 = Path.Combine(pathAbsolute, x64imagePrefix + filename + ".webp");

            if (!Directory.Exists(pathAbsolute))
                Directory.CreateDirectory(pathAbsolute);

            using (var instream = new FileStream(Path.Combine(filehmain.Path, filehmain.Name + filehmain.Extension), FileMode.Open)) // перезаписываем на новый
            {
                instream.Seek(0, SeekOrigin.Begin);

                using (var image = await Image.LoadAsync(instream))
                {
                    await image.SaveAsWebpAsync(path, encoder: new WebpEncoder()
                    {
                        Quality = quality
                    });
                }
            }

            using (var instream = new FileStream(Path.Combine(filex64.Path, filex64.Name + filex64.Extension), FileMode.Open)) // перезаписываем на новый
            {
                instream.Seek(0, SeekOrigin.Begin);

                using (var image = await Image.LoadAsync(instream))
                {
                    await image.SaveAsWebpAsync(pathx64, encoder: new WebpEncoder()
                    {
                        Quality = quality
                    });
                }
            }

            var virtualFile = new File()
            {
                Extension = ".webp",
                Name = filename,
                Path = pathAbsolute,
                Size = Convert.ToDouble(String.Format("{0:0.000}", filehmain.Size)),
                SitePath = pathSite
            };

            // При нужности создаем 64х64 кроппер если нужно
            var x64File = new File()
            {
                Extension = ".webp",
                Name = x64imagePrefix + filename,
                Path = pathAbsolute,
                Size = Convert.ToDouble(String.Format("{0:0.000}", filex64.Size)),
                SitePath = pathSite
            };

            await fileServices.CreateFile(virtualFile);
            await fileServices.CreateFile(x64File);

            var croppedFile = new CroppedImageFile()
            {
                ImageId = virtualFile.Id,
                height = getDefaultCroppImage.height,
                y = getDefaultCroppImage.y,
                width = getDefaultCroppImage.width,
                x = getDefaultCroppImage.x
            };

            var croppedFilex64 = new CroppedImageFile()
            {
                ImageId = x64File.Id,
                height = getDefaultCroppImagex64.height,
                y = getDefaultCroppImagex64.y,
                width = getDefaultCroppImagex64.width,
                x = getDefaultCroppImagex64.x
            };

            await fileServices.CreateCropperFile(croppedFile);
            await fileServices.CreateCropperFile(croppedFilex64);

            return new UserImageInfo() { MainImageId = croppedFile.Id, x64CroppedImageId = croppedFilex64.Id };
        }

        public async Task<UserImageInfo> GenerateAvatarWebpFile(IFileEntry file, double MBsize, CroppedImageInfo croppedImageInfo, string imagePrefix = "img", int quality = 70, int hourseToDelete = -1)
        {
            var filename = imagePrefix + ClientAuthServices.GenerateRandomString(15, false);

            var level1 = ClientAuthServices.GenerateRandomString(2, false).ToUpperInvariant();
            var level2 = ClientAuthServices.GenerateRandomString(2, false).ToUpperInvariant();

            var pathSite = "/imgs/" + "userImages/" + level1 + "/" + level2 + "/";

            var pathAbsolute = Path.Combine(hostingEnvironment.WebRootPath, "imgs", "userImages", level1, level2);

            if (!Directory.Exists(pathAbsolute))
                Directory.CreateDirectory(pathAbsolute);

            var path = Path.Combine(pathAbsolute,
                filename + ".webp");

            var pathx64 = Path.Combine(pathAbsolute,
                x64imagePrefix + filename + ".webp");

            // Создаем основной файл 
            var virtualFile = new File()
            {
                Extension = ".webp",
                Name = filename,
                Path = pathAbsolute,
                Size = Convert.ToDouble(String.Format("{0:0.000}", MBsize)),
                SitePath = pathSite,
                hourseToDelete = hourseToDelete
            };

            // При нужности создаем 64х64 кроппер если нужно
            var x64File = new File()
            {
                Extension = ".webp",
                Name = x64imagePrefix + filename,
                Path = pathAbsolute,
                Size = Convert.ToDouble(String.Format("{0:0.000}", MBsize)),
                SitePath = pathSite,
                hourseToDelete = hourseToDelete
            };

            await fileServices.CreateFile(virtualFile);

            if (croppedImageInfo != null)
            {
                using (var instream = new MemoryStream())
                {
                    await file.WriteToStreamAsync(instream);
                    instream.Seek(0, SeekOrigin.Begin);

                    using (var image = await Image.LoadAsync(instream))
                    {
                        image.Mutate(i => i.Crop(Rectangle.Round(new RectangleF((float)croppedImageInfo.X, (float)croppedImageInfo.Y, (float)croppedImageInfo.Width, (float)croppedImageInfo.Height))));

                        await image.SaveAsWebpAsync(path, encoder: new WebpEncoder()
                        {
                            Quality = quality
                        });
                    }
                }

                var croppedFile = new CroppedImageFile()
                {
                    ImageId = virtualFile.Id,
                    height = croppedImageInfo.Height,
                    y = croppedImageInfo.Y,
                    width = croppedImageInfo.Width,
                    x = croppedImageInfo.X
                };

                await fileServices.CreateCropperFile(croppedFile);

                using (var instream = new MemoryStream())
                {
                    await file.WriteToStreamAsync(instream);
                    instream.Seek(0, SeekOrigin.Begin);

                    using (var image = await Image.LoadAsync(instream))
                    {
                        image.Mutate(i =>
                        {
                            i.Crop(Rectangle.Round(new RectangleF((float)croppedImageInfo.X, (float)croppedImageInfo.Y, (float)croppedImageInfo.Width, (float)croppedImageInfo.Height)));
                            i.Resize(new ResizeOptions()
                            {
                                Mode = ResizeMode.Stretch,
                                Size = new SixLabors.ImageSharp.Size(64, 71)
                            });
                            }
                        );

                        await image.SaveAsWebpAsync(pathx64, encoder: new WebpEncoder()
                        {
                            Quality = quality
                        });
                    }
                }

                // 64x71 = 64x64 но что бы правильно показывались хекасгональные фотки лучше 64x71

                var croppedFilex64 = new CroppedImageFile()
                {
                    ImageId = x64File.Id,
                    height = 64,
                    y = 0,
                    width = 64,
                    x = 0
                };

                await fileServices.CreateCropperFile(croppedFilex64);
            }

            return new UserImageInfo() { MainImageId = virtualFile.Id, x64CroppedImageId = x64File.Id };
        }
    }


    public class VirtualFileStatic
    {

        public VirtualFileStatic(IWebHostEnvironment hostingEnvironment, IVirtualFileServices fileServices)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.fileServices = fileServices;
        }

        public IWebHostEnvironment hostingEnvironment { get; set; }
        public IVirtualFileServices fileServices { get; set; }

        public async Task DeleteFile(int id)
        {
            var mainFile = await fileServices.GetFileById(id);
            var filePathMain = Path.Combine(mainFile.Path, mainFile.Name + mainFile.Extension);
            System.IO.File.Delete(filePathMain); // удаляем старый файл
            await fileServices.DeleteFile(mainFile);
        }


        public async Task<int> GenerateWebpImage(IFileEntry file, double MBsize, string imagePrefix = "img", int quality = 70, int hourseToDelete = -1)
        {
            var filename = imagePrefix + ClientAuthServices.GenerateRandomString(15, false);

            var level1 = ClientAuthServices.GenerateRandomString(2, false).ToUpperInvariant();
            var level2 = ClientAuthServices.GenerateRandomString(2, false).ToUpperInvariant();

            var pathSite = "/imgs/" + "userImages/" + level1 + "/" + level2 + "/";

            var pathAbsolute = Path.Combine(hostingEnvironment.WebRootPath, "imgs", "userImages", level1, level2);

            if (!Directory.Exists(pathAbsolute))
                Directory.CreateDirectory(pathAbsolute);

            var path = Path.Combine(pathAbsolute,
                filename + ".webp");

            // Создаем основной файл 
            var virtualFile = new File()
            {
                Extension = ".webp",
                Name = filename,
                Path = pathAbsolute,
                Size = Convert.ToDouble(String.Format("{0:0.000}", MBsize)),
                SitePath = pathSite,
                hourseToDelete = hourseToDelete
            };

            using (var instream = new MemoryStream())
            {
                await file.WriteToStreamAsync(instream);
                instream.Seek(0, SeekOrigin.Begin);

                using (var image = await Image.LoadAsync(instream))
                {
                    await image.SaveAsWebpAsync(path, encoder: new WebpEncoder()
                    {
                        Quality = quality
                    });
                }
            }

            await fileServices.CreateFile(virtualFile);

            return virtualFile.Id;
        }
    }

    public interface IVirtualFileServices
    {
        Task CreateFile(File file);
        Task<string> GetPhysicFileUrl(int id);
        Task<File> GetFileById(int id);
        Task CreateCropperFile(CroppedImageFile file);
        Task<string> GetPhysicCroppedFile(int id);
        Task<CroppedImageFile> GetCroppedImageFile(int id);
        Task UpdateFileCrooped(CroppedImageFile file);
        Task DeleteFile(File file);
    }

    public class VirtualFileServices : ApplicationService, IVirtualFileServices
    {
        public static string[] imageMimeTypes = new string[] { "image/png", "image/jpeg" };
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly VirtualFileManager _fileManager;
        private readonly VirtualCroopedFileManager _fileCroppedManager;

        public VirtualFileServices(IWebHostEnvironment hostingEnvironment, VirtualFileManager fileManager,
            VirtualCroopedFileManager fileCroppedManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileManager = fileManager;
            _fileCroppedManager = fileCroppedManager;
        }

        public async Task<File> GetFileById(int id) => await _fileManager.GetAsync(id);
        public async Task CreateFile(File file) => await _fileManager.AddAsync(file, true);
        public async Task<string> GetPhysicFileUrl(int id) => await _fileManager.GetPhysicFileUrl(id);
        public async Task<string> GetPhysicCroppedFile(int id) => await _fileCroppedManager.GetPhysicFileCropperUrl(id);
        public async Task CreateCropperFile(CroppedImageFile file) => await _fileCroppedManager.AddAsync(file, true);
        public async Task<CroppedImageFile> GetCroppedImageFile(int id) => await _fileCroppedManager.GetAsync(id);
        public async Task UpdateFileCrooped(CroppedImageFile file) => await _fileCroppedManager.UpdateAsync(file);
        public async Task DeleteFile(File file) => await _fileManager.DeleteAsync(file);
    }
}
