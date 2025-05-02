using Demo.BusinessLogic.Services.AttachmentService.AttachmentService; // This line is incorrect and unnecessary.
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services
{
    public class AttachmentServices : IAttachmentService
    {
        List<string> allowedExtensions =  [ ".jpg", ".png", ".pdf" ];
        const int maxSize = 2 * 1024 * 1024; // 2 MB
        public string? Upload(IFormFile file, string FolderName)
        {
            // 1. Check Extension 

            var extension = Path.GetExtension(path: file.FileName); //.png 
            if (!allowedExtensions.Contains(item: extension)) return null;
            // 2.Check Size 

            if (file.Length == 0 || file.Length > maxSize) return null;
            // 3. Get Located Folder Path 

            //var FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}"; 
            //  var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Files", FolderName);

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            // 4. Make Attachment Name Unique GUID 
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            //5.Get File Path 
            var filePath = Path.Combine(FolderPath, fileName);
            // File Location 
            // 6. Create File Stream To Copy File [Unmanaged] 
            using FileStream fs = new FileStream(path: filePath, mode: FileMode.Create);
            // 7.Use Stream To Copy File 
            file.CopyTo(target: fs);
            //8. Return FileName To Store In Database I 
            return fileName;

        }

        public bool Delete(string filePath)
        {
            if (!File.Exists(path: filePath)) return false;
            else
            {
                File.Delete(path: filePath);
                return true;
            }
        }
    }
}
