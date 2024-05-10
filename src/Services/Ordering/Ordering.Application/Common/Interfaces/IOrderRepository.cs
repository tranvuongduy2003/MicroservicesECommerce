using Contracts.Common;
using Ordering.Domain.Entities;

namespace Ordering.Application.Common.Interfaces
{
    public interface IOrderRepository : IRepositoryBaseAsync<Order, long>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);

        Task<Order> GetOrder(long id);

        Task<long> CreateOrder(Order order);

        Task UpdateOrder(Order order);

        Task DeleteOrder(long id);
    }
}
