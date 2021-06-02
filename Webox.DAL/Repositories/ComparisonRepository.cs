using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.DAL.Database;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;

namespace Webox.DAL.Repositories
{
    public class ComparisonRepository : IRepository<Comparison>
    {
        private readonly DbSet<Comparison> comparisons;

        public ComparisonRepository(ApplicationDbContext dbContext)
        {
            comparisons = dbContext.Comparisons;
        }

        public async Task Add(Comparison entity)
        {
            await Task.Run(() =>
            {
                comparisons.Add(entity);
            });
        }

        public async Task Delete(string id)
        {
            await Task.Run(() =>
            {
                var entity = comparisons.Find(id);
                if (entity != null)
                {
                    comparisons.Remove(entity);
                }
            });
        }

        public async Task<List<Comparison>> GetAll()
        {
            return await Task.Run(() =>
            {
                return comparisons.ToList();
            });
        }

        public async Task<Comparison> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return comparisons.Find(id);
            });
        }

        public async Task Update(Comparison entity)
        {
            await Task.Run(() =>
            {
                comparisons.Update(entity);
            });
        }
    }
}
