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
    public class OrderRepository : IRepository<Order>
    {
        private readonly DbSet<Order> orders;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            orders = dbContext.Orders;
        }

        public async Task Add(Order entity)
        {
            await Task.Run(() =>
            {
                orders.Add(entity);
            });
        }

        public async Task Delete(string id)
        {
            await Task.Run(() =>
            {
                var entity = orders.Find(id);
                if (entity != null)
                {
                    orders.Remove(entity);
                }
            });
        }

        public async Task<List<Order>> GetAll()
        {
            return await Task.Run(() =>
            {
                return orders.ToList();
            });
        }

        public async Task<Order> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return orders.Find(id);
            });
        }

        public async Task Update(Order entity)
        {
            await Task.Run(() =>
            {
                orders.Update(entity);
            });
        }
    }
}
