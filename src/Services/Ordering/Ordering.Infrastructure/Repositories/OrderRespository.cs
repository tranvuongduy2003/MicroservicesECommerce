using Contracts.Common;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRespository : RepositoryBase<Order, long, OrderContext>, IOrderRepository
    {
        public OrderRespository(OrderContext dbContext, IUnitOfWork<OrderContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName) =>
            await FindByCondition(x => x.UserName.Equals(userName)).ToListAsync();

        public Task<Order> GetOrder(long id) => GetByIdAsync(id);

        public Task<long> CreateOrder(Order order) => CreateAsync(order);

        public Task UpdateOrder(Order order) => UpdateAsync(order);

        public async Task DeleteOrder(long id)
        {
            var order = await GetOrder(id);
            if (order != null) DeleteAsync(order);
        }
    }
}
