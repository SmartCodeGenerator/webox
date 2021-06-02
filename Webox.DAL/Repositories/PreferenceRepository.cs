using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.DAL.Database;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;

namespace Webox.DAL.Repositories
{
    public class PreferenceRepository : IRepository<Preference>
    {
        private readonly DbSet<Preference> preferences;

        public PreferenceRepository(ApplicationDbContext dbContext)
        {
            preferences = dbContext.Preferences;
        }

        public async Task Add(Preference entity)
        {
            await Task.Run(() =>
            {
                preferences.Add(entity);
            });
        }

        public async Task Delete(string id)
        {
            await Task.Run(() =>
            {
                var entity = preferences.Find(id);
                if (entity != null)
                {
                    preferences.Remove(entity);
                }
            });
        }

        public async Task<List<Preference>> GetAll()
        {
            return await Task.Run(() =>
            {
                return preferences.ToList();
            });
        }

        public async Task<Preference> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return preferences.Find(id);
            });
        }

        public async Task Update(Preference entity)
        {
            await Task.Run(() =>
            {
                preferences.Update(entity);
            });
        }
    }
}
