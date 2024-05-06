using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common;

namespace Basket.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services) =>
            services.AddScoped<IBasketRepository, BasketRepository>()
                .AddTransient<ISerializeService, ISerializeService>();

        public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetSection("CacheSettings:ConnectionString").Value;
            if (string.IsNullOrEmpty(redisConnectionString))
                throw new ArgumentNullException("Redis Connection string is not configured.");

            // Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            return services;
        }
    }
}
