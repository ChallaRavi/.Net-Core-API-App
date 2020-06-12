using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(string url, int Id);
        Task<IEnumerable<T>> GetAllAsync(string url);

        Task<bool> CeateAsync(string url, T objectCreate);

    }
}
