using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace OnlineLearning.Core.Helpers.Exictance
{
    public static class  FileCreateExtension
    {
        public static string CreateFile(this IFormFile file, string wwwroot, string folderName)
        {
            if (file == null || file.Length == 0)
                return null;

            string extension = Path.GetExtension(file.FileName);
            string baseFileName = Path.GetFileNameWithoutExtension(file.FileName);


            if (baseFileName.Length > 64)
            {
                baseFileName = baseFileName.Substring(baseFileName.Length - 64);
            }


            string filename = $"{Guid.NewGuid()}_{baseFileName}{extension}";



            //string directoryPath = Path.Combine(wwwroot + folderName);//


            //if (!Directory.Exists(directoryPath))
            //{
            //    Directory.CreateDirectory(directoryPath);
            //}

            string filePath = Path.Combine(wwwroot + folderName, filename);


            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return filename;
        }

        public static void RemoveFile(this string filename, string wwwroot, string folderName)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return;

            string filePath = Path.Combine(wwwroot, folderName, filename);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
