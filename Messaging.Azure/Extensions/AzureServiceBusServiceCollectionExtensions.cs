using System;
using Messaging.Azure.Interfaces;
using Messaging.Azure.Models;
using Messaging.Azure.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Azure.Extensions
{
    /// <summary>
    /// Includes a collection of extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class AzureServiceBusServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services for Azure Service Bus to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="azureServiceBusOptionsFunc">The <see cref="Func{T,TResult}"/> to provide <see cref="AzureServiceBusOptions"/></param>
        public static void AddAzureServiceBus(this IServiceCollection services, Func<IServiceProvider, AzureServiceBusOptions> azureServiceBusOptionsFunc)
        {
            services.AddSingleton(azureServiceBusOptionsFunc);
            services.AddSingleton<ITopicClientProvider, TopicClientProvider>();
            services.AddScoped(typeof(ITopicPublisher<>), typeof(AzureTopicPublisher<>));
        }
    }
}
