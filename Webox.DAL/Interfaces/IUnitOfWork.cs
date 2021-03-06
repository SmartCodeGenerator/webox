﻿using System;
using System.Threading.Tasks;
using Webox.DAL.Entities;
using Webox.DAL.Repositories;

namespace Webox.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<StorageLot> StorageLots { get; }
        IRepository<Deliverer> Deliverers { get; }
        IRepository<Laptop> Laptops { get; }
        UserAccountRepository UserAccount { get; }
        IRepository<Review> Reviews { get; }
        IRepository<Comparison> Comparisons { get; }
        IRepository<Preference> Preferences { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
        Task SaveChangesAsync();
    }
}
