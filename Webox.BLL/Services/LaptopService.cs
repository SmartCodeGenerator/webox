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
            if (laptop != null)
            {
                var reviews = new List<ReviewInfoDTO>();
                foreach (var review in laptop.Reviews)
                {
                    reviews.Add(new ReviewInfoDTO
                    {
                        Id = review.ReviewId,
                        PubDateTime = review.PublishDateTime,
                        Rating = review.Rating,
                        Text = review.ReviewText,
                        UserName = await unitOfWork.UserAccount.GetFullName(review.AccountId),
                        LaptopId = review.LaptopId
                    });
                }

                var storageLots = new List<StorageLotInfoDTO>();
                foreach (var storageLot in laptop.StorageLots)
                {
                    storageLots.Add(new StorageLotInfoDTO
                    {
                        StorageLotId = storageLot.StorageLotId,
                        WarehouseAddress = storageLot.WarehouseAddress,
                        SupplyDateTime = storageLot.SupplyDateTime,
                        LaptopsAmount = storageLot.LaptopsAmount,
                        LaptopsCostPerUnit = storageLot.LaptopsCostPerUnit,
                        LaptopId = storageLot.LaptopId,
                        DelivererId = storageLot.DelivererId
                    });
                }

                return new LaptopWithIdDTO
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
                    ModelImagePath = laptop.ModelImagePath,
                    Reviews = reviews.ToArray(),
                    StorageLots = storageLots.ToArray()
                };
            }
            return null;
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
                var filteredLaptops = new List<Laptop>();
                var nameCriterion = queryParams.ModelName.ToLower();
                var criteria = nameCriterion.Split(' ');
                var cleanCriteria = new List<string>();
                foreach (var criterion in criteria)
                {
                    if (!string.IsNullOrEmpty(criterion))
                    {
                        cleanCriteria.Add(criterion);
                    }
                }
                int counter;
                foreach (var laptop in laptops)
                {
                    counter = 0;
                    foreach (var criterion in cleanCriteria)
                    {
                        if (laptop.ModelName.ToLower().Contains(criterion))
                        {
                            counter++;
                        }
                    }
                    if (counter == cleanCriteria.Count)
                    {
                        filteredLaptops.Add(laptop);
                    }
                }
                laptops = filteredLaptops;
            }
            if (queryParams.Manufacturer != null)
            {
                var col = new List<Laptop>();
                foreach (var manufacturer in queryParams.Manufacturer)
                {
                    foreach (var laptop in laptops)
                    {
                        if (laptop.Manufacturer.ToLower().Equals(manufacturer.Trim().ToLower()))
                        {
                            col.Add(laptop);
                        }
                    }
                }
                laptops = col;
            }
            if (queryParams.Processor != null)
            {
                var col = new List<Laptop>();
                foreach (var processor in queryParams.Processor)
                {
                    foreach (var laptop in laptops)
                    {
                        if (laptop.Processor.ToLower().Equals(processor.Trim().ToLower()))
                        {
                            col.Add(laptop);
                        }
                    }
                }
                laptops = col;
            }
            if (queryParams.Graphics != null)
            {
                var col = new List<Laptop>();
                foreach (var graphics in queryParams.Graphics)
                {
                    foreach (var laptop in laptops)
                    {
                        if (laptop.GraphicsCard.ToLower().Equals(graphics.Trim().ToLower()))
                        {
                            col.Add(laptop);
                        }
                    }
                }
                laptops = col;
            }
            if (queryParams.RAM != null)
            {
                var col = new List<Laptop>();
                foreach (var ram in queryParams.RAM)
                {
                    foreach (var laptop in laptops)
                    {
                        if (laptop.RAMCapacity == ram)
                        {
                            col.Add(laptop);
                        }
                    }
                }
                laptops = col;
            }
            if (queryParams.SSD != null)
            {
                var col = new List<Laptop>();
                foreach (var ssd in queryParams.SSD)
                {
                    foreach (var laptop in laptops)
                    {
                        if (laptop.SSDCapacity == ssd)
                        {
                            col.Add(laptop);
                        }
                    }
                }
                laptops = col;
            }
            if (queryParams.Screen != null)
            {
                var col = new List<Laptop>();
                foreach (var screen in queryParams.Screen)
                {
                    foreach (var laptop in laptops)
                    {
                        if (laptop.ScreenSize == screen)
                        {
                            col.Add(laptop);
                        }
                    }
                }
                laptops = col;
            }
            if (queryParams.OS != null)
            {
                var col = new List<Laptop>();
                foreach (var os in queryParams.OS)
                {
                    foreach (var laptop in laptops)
                    {
                        if (laptop.OS.ToLower().Equals(os.Trim().ToLower()))
                        {
                            col.Add(laptop);
                        }
                    }
                }
                laptops = col;
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

        public async Task<List<string>> GetModelNameList(string name)
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            var names = new List<string>();
            var nameCriterion = name.ToLower();
            var criteria = nameCriterion.Split(' ');
            var cleanCriteria = new List<string>();
            foreach (var criterion in criteria)
            {
                if (!string.IsNullOrEmpty(criterion))
                {
                    cleanCriteria.Add(criterion);
                }
            }
            int counter;
            foreach (var laptop in laptops)
            {
                counter = 0;
                foreach (var criterion in cleanCriteria)
                {
                    if (laptop.ModelName.ToLower().Contains(criterion))
                    {
                        counter++;
                    }
                }
                if (counter == cleanCriteria.Count)
                {
                    names.Add(laptop.ModelName);
                }
            }
            return names;
        }

        public async Task<List<string>> GetManufacturers()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Select(l => l.Manufacturer).Distinct().ToList();
        }

        public async Task<List<string>> GetProcessors()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Select(l => l.Processor).Distinct().ToList();
        }

        public async Task<List<string>> GetGraphics()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Select(l => l.GraphicsCard).Distinct().ToList();
        }

        public async Task<List<int>> GetRAM()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Select(l => l.RAMCapacity).Distinct().ToList();
        }

        public async Task<List<int>> GetSSD()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Select(l => l.SSDCapacity).Distinct().ToList();
        }

        public async Task<List<float>> GetScreens()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Select(l => l.ScreenSize).Distinct().ToList();
        }

        public async Task<List<string>> GetOS()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Select(l => l.OS).Distinct().ToList();
        }

        public async Task<float> GetMinWeight()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Min(l => l.Weight);
        }

        public async Task<float> GetMaxWeight()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Max(l => l.Weight);
        }

        public async Task<float> GetMinPrice()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Min(l => l.Price);
        }

        public async Task<float> GetMaxPrice()
        {
            var laptops = await unitOfWork.Laptops.GetAll();
            return laptops.Max(l => l.Price);
        }
    }
}
