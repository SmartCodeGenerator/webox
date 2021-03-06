﻿using Microsoft.EntityFrameworkCore;
using Webox.DAL.Entities;

namespace Webox.DAL.Database
{
    public class ApplicationDataContext : DbContext
    {
        public DbSet<UserAccount> Accounts { get; set; }
        public DbSet<StorageLot> StorageLots { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Deliverer> Deliverers { get; set; }
        public DbSet<Comparison> Comparisons { get; set; }

        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}