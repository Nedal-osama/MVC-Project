using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace MVC_03.PL.Helpers
{
    public class DocumentSettings
    {
        public static async Task<string> UploadFileAsync(IFormFile file ,string folderName)
        {
            //1
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //2
            string fileName = $"{Guid.NewGuid()}{file.FileName}";


            //3
            string filePath = Path.Combine(folderPath, fileName);

            //4
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
         
        }
        public static void DeletFile(string fileName ,string folderName)
        {
            string filePath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName,fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
