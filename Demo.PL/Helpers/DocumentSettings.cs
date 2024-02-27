using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploaFile(IFormFile file, string folderName)
        {

            //1. Get Located Folder Path
            //string folderPath = "C:\\Users\\20101\\Desktop\\Route\\DotNet\\6. ASP\\Session 5\\Ass\\Demo.PL\\Demo.PL\\wwwroot\\Files\\";
            //string folderPath = Directory.GetCurrentDirectory()+ "\\wwwroot\\Files\\"+ folderName;
             string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            //2. Get file name and make it unique
            //string fileName = file.FileName;
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            //3. Get File Path

            string filePath= Path.Combine(folderPath, fileName);

            //4.save file as streams (data per time)

           using var fs = new FileStream(filePath, FileMode.Create); //using here is to close connection after using the file

            file.CopyToAsync(fs);
            return fileName;

        }

        public static void DeleteFile(string fileName , string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
