using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Webox.DAL.Database;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;

namespace Webox.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private DelivererRepository delivererRepository;
        private StorageLotRepository storageLotRepository;
        private LaptopRepository laptopRepository;
        private ReviewRepository reviewRepository;
        private UserAccountRepository userAccount;
        private bool disposed = false;

        public UnitOfWork(DbContextOptions<ApplicationDbContext> options)
        {
            context = new ApplicationDbContext(options);
        }

        public IRepository<StorageLot> StorageLots
        {
            get
            {
                if (storageLotRepository == null)
                {
                    storageLotRepository = new StorageLotRepository(context);
                }
                return storageLotRepository;
            }
        }

        public IRepository<Deliverer> Deliverers
        {
            get
            {
                if (delivererRepository == null)
                {
                    delivererRepository = new DelivererRepository(context);
                }
                return delivererRepository;
            }
        }

        public IRepository<Laptop> Laptops
        {
            get
            {
                if (laptopRepository == null)
                {
                    laptopRepository = new LaptopRepository(context);
                }
                return laptopRepository;
            }
        }

        public IRepository<Review> Reviews
        {
            get
            {
                if (reviewRepository == null)
                {
                    reviewRepository = new ReviewRepository(context);
                }
                return reviewRepository;
            }
        }

        public UserAccountRepository UserAccount
        {
            get
            {
                if (userAccount == null)
                {
                    userAccount = new UserAccountRepository(context);
                }
                return userAccount;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
