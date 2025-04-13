using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Services
{
    public interface IImageStorageService
    {
        Task<string> UploadImage(string name, IFormFile file, string containerName);
        Task DeleteImage(string name, string containerName);
    }
}
