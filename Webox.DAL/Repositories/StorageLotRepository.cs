using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.DAL.Database;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;

namespace Webox.DAL.Repositories
{
    public class StorageLotRepository : IRepository<StorageLot>
    {
        private readonly DbSet<StorageLot> storageLots;

        public StorageLotRepository(ApplicationDbContext context)
        {
            storageLots = context.StorageLots;
        }

        public async Task Add(StorageLot entity)
        {
            await Task.Run(() =>
            {
                storageLots.Add(entity);
            });
        }

        public async Task Delete(string id)
        {
            await Task.Run(() =>
            {
                var entity = storageLots.Find(id);
                if (entity != null)
                {
                    storageLots.Remove(entity);
                }
            });
        }

        public async Task<List<StorageLot>> GetAll()
        {
            return await Task.Run(() =>
            {
                return storageLots.ToList();
            });
        }

        public async Task<StorageLot> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return storageLots.Find(id);
            });
        }

        public async Task Update(StorageLot entity)
        {
            await Task.Run(() =>
            {
                storageLots.Update(entity);
            });
        }
    }
}
