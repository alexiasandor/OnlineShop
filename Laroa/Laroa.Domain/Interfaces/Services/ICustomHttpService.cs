using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Services
{
    public interface ICustomHttpService
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object data);
        Task<T> PatchAsync<T>(string url, object data);
        Task<T> DeleteAsync<T>(string url);
    }
}
