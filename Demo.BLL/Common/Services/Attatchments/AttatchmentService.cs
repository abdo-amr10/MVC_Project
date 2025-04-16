using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Common.Services.Attatchments
{
    public class AttatchmentService : IAttatchmentService
    {
        public readonly List<string> _allowedExtension = new()
        {
            ".jpg" , ".png" , ".jpeg"
        };
        public const int _allowedMaxSize = 2_097_152;
        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            // 1] Validation For File Extensions => {".png", ".jpg" , ".jpeg"}

            var extension = Path.GetExtension(file.FileName); // Doaa.jpeg

            if (!_allowedExtension.Contains(extension))
                return null;

            // 2] Validation For Max Size [2GB]
            if (file.Length > _allowedMaxSize)
                return null;

            // 3] Get Located Folder Path

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 4] Must Image Name Be Unique
            var fileName = $"{Guid.NewGuid()}{extension}";

            // 5] Get FilePath [FolderPath + FileName]
            var filePath = Path.Combine(folderPath, fileName);

            // 6] Save File As Streams [Data per Time]

            var fileStream = new FileStream(filePath, FileMode.Create);

            // 7] Copy File To FileStream

            await file.CopyToAsync(fileStream);

            // 8] Return FileName
            return fileName;


        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }

    }
}
