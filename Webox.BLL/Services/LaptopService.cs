using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Infrastructure;
using Webox.BLL.Infrastructure.QueryParams;
using Webox.BLL.Interfaces;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;

namespace Webox.BLL.Services
{
    public class LaptopService : ILaptopService
    {
        private readonly IUnitOfWork unitOfWork;

        public LaptopService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddLaptop(LaptopDTO data)
        {
            var laptop = new Laptop
            {
                ModelName = data.ModelName,
                Manufacturer = data.Manufacturer,
                Processor = data.Processor,
                GraphicsCard = data.Graphic,
                RAMCapacity = data.Ram,
                SSDCapacity = data.Ssd,
                ScreenSize = data.Screen,
                OS = data.Os,
                Weight = data.Weight,
                Price = data.Price,
                Rating = 0,
                IsAvailable = false,
                ModelImagePath = data.ModelImagePath
            };

            await unitOfWork.Laptops.Add(laptop);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteLaptop(string id)
        {
            await unitOfWork.Laptops.Delete(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<LaptopWithIdDTO> GetById(string id)
        {
            var laptop = await unitOfWork.Laptops.GetById(id);

            return laptop != null ? new LaptopWithIdDTO
            {
                Id = laptop.LaptopId,
                ModelName = laptop.ModelName,
                Manufacturer = laptop.Manufacturer,
                Processor = laptop.Processor,
                Graphic = laptop.GraphicsCard,
                Ram = laptop.RAMCapacity,
                Ssd = laptop.SSDCapacity,
                Screen = laptop.ScreenSize,
                Os = laptop.OS,
                Weight = laptop.Weight,
                Price = laptop.Price,
                Rating = laptop.Rating,
                IsAvailable = laptop.IsAvailable,
                ModelImagePath = laptop.ModelImagePath
            } : null;
        }

        public async Task<PaginatedList<LaptopWithIdDTO>> Index(SortOrder sortOrder, LaptopParams queryParams, int pageIndex)
        {
            var laptops = await unitOfWork.Laptops.GetAll();

            switch (sortOrder)
            {
                case SortOrder.SortByRatingDescending:
                    laptops = laptops.OrderByDescending(l => l.Rating).ToList();
                    break;
                case SortOrder.SortByPriceAscending:
                    laptops = laptops.OrderBy(l => l.Price).ToList();
                    break;
                case SortOrder.SortByPriceDescending:
                    laptops = laptops.OrderByDescending(l => l.Price).ToList();
                    break;
            }

            if (!string.IsNullOrEmpty(queryParams.ModelName))
            {
                laptops = laptops.Where(l => l.ModelName.ToLower().Contains(queryParams.ModelName.Trim().ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(queryParams.Manufacturer))
            {
                laptops = laptops.Where(l => l.Manufacturer.ToLower().Equals(queryParams.Manufacturer.Trim().ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(queryParams.Processor))
            {
                laptops = laptops.Where(l => l.Processor.ToLower().StartsWith(queryParams.Processor.Trim().ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(queryParams.Graphics))
            {
                laptops = laptops.Where(l => l.GraphicsCard.ToLower().StartsWith(queryParams.Graphics.Trim().ToLower())).ToList();
            }
            if (queryParams.RAM != 0)
            {
                laptops = laptops.Where(l => l.RAMCapacity == queryParams.RAM).ToList();
            }
            if (queryParams.SSD != 0)
            {
                laptops = laptops.Where(l => l.SSDCapacity == queryParams.SSD).ToList();
            }
            if (queryParams.Screen != 0)
            {
                laptops = laptops.Where(l => l.ScreenSize == queryParams.Screen).ToList();
            }
            if (!string.IsNullOrEmpty(queryParams.OS))
            {
                laptops = laptops.Where(l => l.OS.ToLower().StartsWith(queryParams.OS.Trim().ToLower())).ToList();
            }
            if (queryParams.MinWeight <= queryParams.MaxWeight && queryParams.MaxWeight != 0)
            {
                laptops = laptops.Where(l => l.Weight <= queryParams.MaxWeight && l.Weight >= queryParams.MinWeight).ToList();
            }
            if (queryParams.MinPrice <= queryParams.MaxPrice && queryParams.MaxPrice != 0)
            {
                laptops = laptops.Where(l => l.Price <= queryParams.MaxPrice && l.Price >= queryParams.MinPrice).ToList();
            }

            var data = new List<LaptopWithIdDTO>();
            laptops.ForEach((e) =>
            {
                data.Add(new LaptopWithIdDTO
                {
                    Id = e.LaptopId,
                    ModelName = e.ModelName,
                    Manufacturer = e.Manufacturer,
                    Processor = e.Processor,
                    Graphic = e.GraphicsCard,
                    Ram = e.RAMCapacity,
                    Ssd = e.SSDCapacity,
                    Screen = e.ScreenSize,
                    Os = e.OS,
                    Weight = e.Weight,
                    Price = e.Price,
                    Rating = e.Rating,
                    IsAvailable = e.IsAvailable,
                    ModelImagePath = e.ModelImagePath
                });
            });

            return PaginatedList<LaptopWithIdDTO>.Create(data, pageIndex, 14);
        }

        public async Task UpdateLaptop(string id, LaptopDTO data)
        {
            var entity = await unitOfWork.Laptops.GetById(id);

            entity.ModelName = data.ModelName;
            entity.Manufacturer = data.Manufacturer;
            entity.Processor = data.Processor;
            entity.GraphicsCard = data.Graphic;
            entity.RAMCapacity = data.Ram;
            entity.SSDCapacity = data.Ssd;
            entity.ScreenSize = data.Screen;
            entity.OS = data.Os;
            entity.Weight = data.Weight;
            entity.Price = data.Price;
            entity.ModelImagePath = data.ModelImagePath;

            await unitOfWork.Laptops.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
