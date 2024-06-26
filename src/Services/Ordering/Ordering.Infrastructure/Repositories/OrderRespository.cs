﻿using Contracts.Common;
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

        public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName) =>
            await FindByCondition(x => x.UserName.Equals(userName)).ToListAsync();

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await CreateAsync(order);
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            await UpdateAsync(order);
            return order;
        }
    }
}
