using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HiddenVilla_Server.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;

        public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
        }
        public bool DeleteFile(string fileName)
        {
            bool status = false;
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\RoomImages\\{fileName}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                     status = true;
                    return status;
                }
                else
                {
                     status = false;
                    return status;
                }
            }catch (Exception ex)
            {
                throw ex;
            }

           
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString()+fileInfo.Extension;
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\RoomImages";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "RoomImages", fileName);

                var memoryStream = new MemoryStream();
                await file.OpenReadStream(maxAllowedSize: long.MaxValue).CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }
              
                    await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        memoryStream.WriteTo(fs);
                    }
                    var url = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host.Value}/";
                var fullPath = $"{url}RoomImages/{fileName}";

                return fullPath;

                

            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
