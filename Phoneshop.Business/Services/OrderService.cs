using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoneshop.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateAsync(Order order)
        {
            if (string.IsNullOrEmpty(order.CustomerId))
                throw new Exception("Missing the CustomerId");

            if (order.ProductsPerOrder == null)
                throw new Exception("Missing the phones ordered");

            await _orderRepository.CreateAsync(order);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var order = await _orderRepository.GetByIdAsync(id);
            order.Deleted = true;
            //order.Reason = 1;

            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }

        public IEnumerable<Order> GetAllByUser(string userId)
        {
            return _orderRepository.GetAll().Where(x => x.CustomerId == userId).AsEnumerable();
        }

        public async Task<Order> GetByIdAsync(int id, string userId)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order.CustomerId != userId)
                throw new ArgumentException("This order does not belong to this userId!");

            return order;
        }
    }
}
