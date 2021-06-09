using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.DAL.Database;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;

namespace Webox.DAL.Repositories
{
    public class LaptopRepository : IRepository<Laptop>
    {
        private readonly DbSet<Laptop> laptops;

        public LaptopRepository(ApplicationDbContext context)
        {
            laptops = context.Laptops;
        }

        public async Task Add(Laptop entity)
        {
            await Task.Run(() =>
            {
                laptops.Add(entity);
            });
        }

        public async Task Delete(string id)
        {
            await Task.Run(() =>
            {
                var entity = laptops.Find(id);
                if (entity != null)
                {
                    laptops.Remove(entity);
                }
            });
        }

        public async Task<List<Laptop>> GetAll()
        {
            return await Task.Run(() =>
            {
                return laptops.ToList();
            });
        }

        public async Task<Laptop> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return laptops.Include(l => l.Reviews).Include(l => l.StorageLots).ToList().First(l => l.LaptopId.Equals(id));
            });
        }

        public async Task Update(Laptop entity)
        {
            await Task.Run(() =>
            {
                laptops.Update(entity);
            });
        }
    }
}
