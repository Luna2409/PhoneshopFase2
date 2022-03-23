using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoneshop.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(int id, string userId);

        IEnumerable<Order> GetAllByUser(string userId);

        Task CreateAsync(Order order);

        Task DeleteAsync(int id);
    }
}
