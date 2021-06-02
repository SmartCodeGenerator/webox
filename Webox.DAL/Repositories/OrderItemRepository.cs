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
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly DbSet<OrderItem> orderItems;

        public OrderItemRepository(ApplicationDbContext dbContext)
        {
            orderItems = dbContext.OrderItems;
        }

        public async Task Add(OrderItem entity)
        {
            await Task.Run(() =>
            {
                orderItems.Add(entity);
            });
        }

        public async Task Delete(string id)
        {
            await Task.Run(() =>
            {
                var entity = orderItems.Find(id);
                if (entity != null)
                {
                    orderItems.Remove(entity);
                }
            });
        }

        public async Task<List<OrderItem>> GetAll()
        {
            return await Task.Run(() =>
            {
                return orderItems.ToList();
            });
        }

        public async Task<OrderItem> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return orderItems.Find(id);
            });
        }

        public async Task Update(OrderItem entity)
        {
            await Task.Run(() =>
            {
                orderItems.Update(entity);
            });
        }
    }
}
