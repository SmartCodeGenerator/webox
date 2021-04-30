using System;
using System.Threading.Tasks;
using Webox.DAL.Entities;

namespace Webox.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<StorageLot> StorageLots { get; }
        IRepository<Deliverer> Deliverers { get; }
        IRepository<Laptop> Laptops { get; }
        Task SaveChangesAsync();
    }
}
