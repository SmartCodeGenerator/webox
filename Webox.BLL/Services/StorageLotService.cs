using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Interfaces;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;
using static Webox.BLL.Infrastructure.Delegates;

namespace Webox.BLL.Services
{
    public class StorageLotService : IStorageLotService
    {
        private readonly IRepository<StorageLot> storageLotRepository;
        private readonly IRepository<Laptop> laptopRepository;
        private readonly SaveChangesAsync saveChangesAsync;

        public StorageLotService(IUnitOfWork unitOfWork)
        {
            storageLotRepository = unitOfWork.StorageLots;
            laptopRepository = unitOfWork.Laptops;
            saveChangesAsync = unitOfWork.SaveChangesAsync;
        }

        public async Task<int> GetLaptopStorageAmount(string laptopId)
        {
            return (await storageLotRepository.GetAll())
                .Where(sl => sl.LaptopId.Equals(laptopId))
                .Sum(sl => sl.LaptopsAmount);
        }

        public async Task SaveStorageLot(StorageLotDTO data)
        {
            var entity = new StorageLot
            {
                WarehouseAddress = data.WarehouseAddress,
                LaptopsCostPerUnit = data.LaptopsCostPerUnit,
                LaptopId = data.LaptopId,
                DelivererId = data.DelivererId,
            };

            await storageLotRepository.Add(entity);
            await saveChangesAsync();
        }

        public async Task UpdateStorageLot(string id, StorageLotDTO data)
        {
            var entity = await storageLotRepository.GetById(id);
            if (entity != null)
            {
                entity.WarehouseAddress = data.WarehouseAddress;
                entity.LaptopsCostPerUnit = data.LaptopsCostPerUnit;
                if (!data.LaptopId.Equals(entity.LaptopId))
                {
                    entity.LaptopsAmount = 0;
                }
                entity.LaptopId = data.LaptopId;
                entity.DelivererId = data.DelivererId;

                await storageLotRepository.Update(entity);
                await saveChangesAsync();

                var amount = await GetLaptopStorageAmount(entity.LaptopId);
                var laptop = await laptopRepository.GetById(entity.LaptopId);
                if (laptop != null)
                {
                    laptop.IsAvailable = amount > 0;
                    await laptopRepository.Update(laptop);
                    await saveChangesAsync();
                }
            }
        }

        public async Task ReplenishStorageLot(string id, StorageReplenishmentDTO data)
        {
            var entity = await storageLotRepository.GetById(id);
            if (entity != null)
            {
                entity.SupplyDateTime = DateTime.UtcNow.AddDays(3);
                entity.LaptopsAmount += data.LaptopsAmount;

                await storageLotRepository.Update(entity);
                await saveChangesAsync();

                var amount = await GetLaptopStorageAmount(entity.LaptopId);
                var laptop = await laptopRepository.GetById(entity.LaptopId);
                if (laptop != null)
                {
                    laptop.IsAvailable = amount > 0;
                    await laptopRepository.Update(laptop);
                    await saveChangesAsync();
                }
            }
        }

        public async Task DeleteStorageLot(string id)
        {
            var entity = await storageLotRepository.GetById(id);
            if (entity != null)
            {
                var laptopId = entity.LaptopId;
                await storageLotRepository.Delete(id);
                await saveChangesAsync();

                var amount = await GetLaptopStorageAmount(laptopId);
                var laptop = await laptopRepository.GetById(laptopId);
                if (laptop != null)
                {
                    laptop.IsAvailable = amount > 0;
                    await laptopRepository.Update(laptop);
                    await saveChangesAsync();
                }
            }
        }

        public async Task<List<StorageLotInfoDTO>> GetStorageLots()
        {
            var data = await storageLotRepository.GetAll();
            var storageLots = new List<StorageLotInfoDTO>();
            foreach (var entry in data)
            {
                storageLots.Add(new StorageLotInfoDTO
                {
                    StorageLotId = entry.StorageLotId,
                    WarehouseAddress = entry.WarehouseAddress,
                    SupplyDateTime = entry.SupplyDateTime,
                    LaptopsAmount = entry.LaptopsAmount,
                    LaptopsCostPerUnit = entry.LaptopsCostPerUnit,
                    LaptopId = entry.LaptopId,
                    DelivererId = entry.DelivererId,
                });
            }
            return storageLots;
        }

        public async Task<StorageLotInfoDTO> GetStorageLot(string id)
        {
            var entity = await storageLotRepository.GetById(id);
            return entity != null ? new StorageLotInfoDTO
            {
                StorageLotId = entity.StorageLotId,
                WarehouseAddress = entity.WarehouseAddress,
                SupplyDateTime = entity.SupplyDateTime,
                LaptopsAmount = entity.LaptopsAmount,
                LaptopsCostPerUnit = entity.LaptopsCostPerUnit,
                LaptopId = entity.LaptopId,
                DelivererId = entity.DelivererId,
            } : null;
        }
    }
}
