using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttatchmentServices
{
    public interface IAttatchmentService
    {
        public string? Upload(IFormFile file, string FolderName);

        public bool Delete(string FilePath);
    }
}
