using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Interfaces;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;
using Webox.DAL.Repositories;
using static Webox.BLL.Infrastructure.Delegates;

namespace Webox.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<OrderItem> orderItemRepository;
        private readonly UserAccountRepository userAccountRepository;
        private readonly UserManager<UserAccount> userManager;
        private readonly SaveChangesAsync saveChangesAsync;

        public OrderService(IUnitOfWork unitOfWork, UserManager<UserAccount> userManager)
        {
            orderRepository = unitOfWork.Orders;
            orderItemRepository = unitOfWork.OrderItems;
            userAccountRepository = unitOfWork.UserAccount;
            this.userManager = userManager;
            saveChangesAsync = unitOfWork.SaveChangesAsync;
        }

        public async Task MakeOrder(string userName, OrderDTO data)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var entity = new Order
                {
                    DeliveryAddress = data.DeliveryAddress,
                    DeliveryDateTime = DateTime.UtcNow.AddDays(3),
                    PlacementDateTime = DateTime.UtcNow,
                    Price = data.Price,
                    AccountId = user.Id
                };

                await orderRepository.Add(entity);
                await saveChangesAsync();

                foreach (var orderItem in data.OrderItems)
                {
                    var childEntity = new OrderItem
                    {
                        Amount = orderItem.Amount,
                        Order = entity,
                        LaptopId = orderItem.LaptopId
                    };
                    await orderItemRepository.Add(childEntity);
                }
                await saveChangesAsync();
            }
        }

        public async Task CancelOrder(string id)
        {
            await orderRepository.Delete(id);
            await saveChangesAsync();
        }

        public async Task<List<OrderInfoDTO>> GetOrders(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var data = await userAccountRepository.GetUserOrders(user.Id);
                var orders = new List<OrderInfoDTO>();
                foreach (var pair in data)
                {
                    var entry = await orderRepository.GetById(pair.Key);
                    var order = new OrderInfoDTO
                    {
                        OrderId = entry.OrderId,
                        DeliveryAddress = entry.DeliveryAddress,
                        DeliveryDateTime = entry.DeliveryDateTime,
                        PlacementDateTime = entry.PlacementDateTime,
                        Price = entry.Price,
                        AccountId = entry.AccountId
                    };
                    int counter = 0;
                    foreach (var item in pair.Value)
                    {
                        order.OrderItems[counter++] = new OrderItemDTO
                        {
                            Amount = item.Amount,
                            LaptopId = item.LaptopId
                        };
                    }
                    orders.Add(order);
                }
                return orders;
            }
            return null;
        }

        public async Task<OrderInfoDTO> GetOrder(string id)
        {
            var data = await orderRepository.GetById(id);
            if (data != null)
            {
                var order = new OrderInfoDTO
                {
                    OrderId = data.OrderId,
                    DeliveryAddress = data.DeliveryAddress,
                    DeliveryDateTime = data.DeliveryDateTime,
                    PlacementDateTime = data.PlacementDateTime,
                    Price = data.Price,
                    AccountId = data.AccountId
                };
                int counter = 0;
                var items = (await orderItemRepository.GetAll()).Where(oi => oi.OrderId.Equals(id));
                foreach (var item in items)
                {
                    order.OrderItems[counter++] = new OrderItemDTO
                    {
                        Amount = item.Amount,
                        LaptopId = item.LaptopId
                    };
                }
                return order;
            }
            return null;
        }
    }
}
