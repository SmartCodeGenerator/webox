using System.Collections.Generic;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Interfaces;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;
using static Webox.BLL.Infrastructure.Delegates;

namespace Webox.BLL.Services
{
    public class DelivererService : IDelivererService
    {
        private readonly IRepository<Deliverer> repository;
        private readonly SaveChangesAsync saveChangesAsync;

        public DelivererService(IUnitOfWork unitOfWork)
        {
            saveChangesAsync = unitOfWork.SaveChangesAsync;
            repository = unitOfWork.Deliverers;
        }

        public async Task SaveDeliverer(DelivererDTO data)
        {
            var entity = new Deliverer
            {
                CompanyName = data.CompanyName,
                PhoneNumber = data.PhoneNumber,
                MainOfficeAddress = data.MainOfficeAddress,
            };

            await repository.Add(entity);
            await saveChangesAsync();
        }

        public async Task UpdateDeliverer(string id, DelivererDTO data)
        {
            var entity = await repository.GetById(id);

            entity.CompanyName = data.CompanyName;
            entity.PhoneNumber = data.PhoneNumber;
            entity.MainOfficeAddress = data.MainOfficeAddress;

            await repository.Update(entity);
            await saveChangesAsync();
        }

        public async Task DeleteDeliverer(string id)
        {
            await repository.Delete(id);
            await saveChangesAsync();
        }

        public async Task<List<DelivererInfoDTO>> GetDeliverers()
        {
            var deliverers = await repository.GetAll();
            var data = new List<DelivererInfoDTO>();
            foreach (var deliverer in deliverers)
            {
                data.Add(new DelivererInfoDTO
                {
                    DelivererId = deliverer.DelivererId,
                    CompanyName = deliverer.CompanyName,
                    PhoneNumber = deliverer.PhoneNumber,
                    MainOfficeAddress = deliverer.MainOfficeAddress,
                });
            }
            return data;
        }

        public async Task<DelivererInfoDTO> GetDeliverer(string id)
        {
            var deliverer = await repository.GetById(id);
            return deliverer != null ? new DelivererInfoDTO
            {
                DelivererId = deliverer.DelivererId,
                CompanyName = deliverer.CompanyName,
                PhoneNumber = deliverer.PhoneNumber,
                MainOfficeAddress = deliverer.MainOfficeAddress,
            } : null;
        }
    }
}
