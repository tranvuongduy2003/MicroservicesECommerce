using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common;
using EventBus.Messages.Interfaces;
using Infrastructure.Extentions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Configurations;

namespace Basket.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusSettings = configuration.GetSection(nameof(EventBusSettings))
                .Get<EventBusSettings>();
            services.AddSingleton(eventBusSettings);

            var cacheSettings = configuration.GetSection(nameof(CacheSettings))
                .Get<CacheSettings>();
            services.AddSingleton(cacheSettings);

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services) =>
            services.AddScoped<IBasketRepository, BasketRepository>()
                .AddTransient<ISerializeService, ISerializeService>();

        public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.GetOptions<CacheSettings>("CacheSettings");
            if (string.IsNullOrEmpty(settings.ConnectionString))
                throw new ArgumentNullException("Redis Connection string is not configured.");

            // Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = settings.ConnectionString;
            });

            return services;
        }

        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
        {
            var settings = services.GetOptions<EventBusSettings>("EventBusSettings");
            if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
                throw new ArgumentNullException("EventBusSettings is not configured.");

            var mqConnection = new Uri(settings.HostAddress);
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection);
                });
                // Publish submit order message
                config.AddRequestClient<IBasketCheckoutEvent>();
            });

            return services;
        }
    }
}
