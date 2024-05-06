using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _serializeService;
        private readonly ILogger _logger;

        public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, ILogger logger)
        {
            _redisCacheService = redisCacheService;
            _serializeService = serializeService;
            _logger = logger;
        }

        public async Task<Cart?> GetBasketByUserName(string username)
        {
            _logger.Information($"BEGIN: GetBasketByUserName {username}");
            var baskets = await _redisCacheService.GetStringAsync(username);
            _logger.Information($"END: GetBasketByUserName {username}");
            return string.IsNullOrEmpty(baskets) ? null : _serializeService.Deserialize<Cart>(baskets);
        }

        public async Task<Cart?> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {
            _logger.Information($"BEGIN: UpdateBasket for {cart.Username}");

            if (options != null)
                await _redisCacheService.SetStringAsync(cart.Username,
                    _serializeService.Serialize(cart), options);
            else
                await _redisCacheService.SetStringAsync(cart.Username,
                    _serializeService.Serialize(cart));

            _logger.Information($"END: UpdateBasket for {cart.Username}");

            return await GetBasketByUserName(cart.Username);
        }

        public async Task<bool> DeleteBasketFromUserName(string userName)
        {
            try
            {
                await _redisCacheService.RemoveAsync(userName);

                return true;
            }
            catch (Exception e)
            {
                _logger.Error("DeleteBasketFromUserName: " + e.Message);
                throw;
            }
        }
    }
}
