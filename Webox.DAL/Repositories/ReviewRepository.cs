using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.DAL.Database;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;

namespace Webox.DAL.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly DbSet<Review> reviews;

        public ReviewRepository(ApplicationDbContext dbContext)
        {
            reviews = dbContext.Reviews;
        }

        public async Task Add(Review entity)
        {
            await Task.Run(() =>
            {
                reviews.Add(entity);
            });
        }

        public async Task Delete(string id)
        {
            await Task.Run(() =>
            {
                var entity = reviews.Find(id);
                if (entity != null)
                {
                    reviews.Remove(entity);
                }
            });
        }

        public async Task<List<Review>> GetAll()
        {
            return await Task.Run(() =>
            {
                return reviews.OrderByDescending(r => r.PublishDateTime).ToList();
            });
        }

        public async Task<Review> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return reviews.Find(id);
            });
        }

        public async Task Update(Review entity)
        {
            await Task.Run(() => 
            {
                reviews.Update(entity);
            });
        }
    }
}
