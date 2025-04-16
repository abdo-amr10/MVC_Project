using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Common.Services.Attatchments
{
    public interface IAttatchmentService
    {
        // Upload , Delete

        Task<string?> UploadAsync(IFormFile file, string folderName);
        bool Delete(string filePath);
    }
}
