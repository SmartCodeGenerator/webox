using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.DAL.Database;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;

namespace Webox.DAL.Repositories
{
    public class DelivererRepository : IRepository<Deliverer>
    {
        private readonly DbSet<Deliverer> deliverers;

        public DelivererRepository(ApplicationDbContext context)
        {
            deliverers = context.Deliverers;
        }

        public async Task Add(Deliverer entity)
        {
            await Task.Run(() =>
            {
                deliverers.Add(entity);
            });
        }

        public async Task Delete(string id)
        {
            await Task.Run(() =>
            {
                var entity = deliverers.Find(id);
                if (entity != null)
                {
                    deliverers.Remove(entity);
                }
            });
        }

        public async Task<List<Deliverer>> GetAll()
        {
            return await Task.Run(() =>
            {
                return deliverers.ToList();
            });
        }

        public async Task<Deliverer> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return deliverers.Find(id);
            });
        }

        public async Task Update(Deliverer entity)
        {
            await Task.Run(() =>
            {
                deliverers.Update(entity);
            });
        }
    }
}
