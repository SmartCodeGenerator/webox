using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webox.DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(string id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(string id);
    }
}
