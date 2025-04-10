using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttatchmentServices
{
    public class AttatchmentService : IAttatchmentService
    {
        List<string> allowedExtensions = [".png", ".jpg", ".Jpeg"];
        const int maxSize = 2_097_152;
        public bool Delete(string FilePath)
        {
            if(!File.Exists(FilePath)) return false;
            else
            {
                File.Delete(FilePath);
                return true;
            }
        }

        public string ? Upload(IFormFile file, string FolderName)
        {
            var extension = Path.GetExtension(file.Name);
            if (!allowedExtensions.Contains(extension)) return null;
            if(file.Length==0||file.Length>maxSize) return null;

            var FolderPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files",FolderName);

            var fileName=$"{Guid.NewGuid()}_${file.FileName}";


            var filePath=Path.Combine(FolderPath, fileName);

            using FileStream fs = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fs);

            return fileName;





            
        }
    }
}
