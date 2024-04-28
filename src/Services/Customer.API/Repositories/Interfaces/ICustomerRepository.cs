using Contracts.Common;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryQueryAsync<Entities.Customer, int, CustomerContext>
    {
        Task<Entities.Customer> GetCustomerByUserNameAsync(string userName);
    }
}
