﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Webox.DAL.Database.EntityConfigurations;
using Webox.DAL.Entities;

namespace Webox.DAL.Database
{
    public class ApplicationDbContext : IdentityDbContext<UserAccount>
    {
        public DbSet<StorageLot> StorageLots { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Deliverer> Deliverers { get; set; }
        public DbSet<Comparison> Comparisons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
            modelBuilder.ApplyConfiguration(new StorageLotConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new PreferenceConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new LaptopConfiguration());
            modelBuilder.ApplyConfiguration(new DelivererConfiguration());
            modelBuilder.ApplyConfiguration(new ComparisonConfiguration());
        }
    }
}
