using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace NewsApi.Api.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<string> GetLocalPath(IFormFile file, IHostEnvironment env)
        {
            if (file is null || file.Length <= 0)
                return null;

            var uploadsDirName = "uploads";
            var uploadsDir = Path.Combine(env.ContentRootPath, uploadsDirName);
            var filepath = Path.Combine(uploadsDir, file.FileName);
            Directory.CreateDirectory(uploadsDir);

            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filepath;
        }
    }
}