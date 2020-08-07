using System;
using System.Collections.Concurrent;
using Messaging.Azure.Interfaces;
using Messaging.Azure.Models;
using Messaging.Exceptions;
using Microsoft.Azure.ServiceBus;

namespace Messaging.Azure.Providers
{
    /// <inheritdoc/>
    public class TopicClientProvider : ITopicClientProvider
    {
        private static readonly ConcurrentDictionary<string, ITopicClient> TopicClients =
            new ConcurrentDictionary<string, ITopicClient>();

        private readonly AzureServiceBusOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicClientProvider"/> class.
        /// </summary>
        /// <param name="options">The Azure Service Bus options</param>
        public TopicClientProvider(AzureServiceBusOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            ValidateServiceBusConnectionString(options.ConnectionString);
        }

        /// <inheritdoc/>
        public ITopicClient Get(string topicName)
        {
            var client = TopicClients.GetOrAdd(
                topicName,
                name =>
                {
                    var topicClient = new TopicClient(_options.ConnectionString, topicName);
                    return topicClient;
                });
            return client;
        }

        private void ValidateServiceBusConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new MessagingException("Azure Service Bus connection string is not configured.");
            }

            try
            {
                var serviceBusConnectionStringBuilder = new ServiceBusConnectionStringBuilder(connectionString);
            }
            catch (ArgumentException e)
            {
                var exception =
                    new MessagingException("The provided Azure Service Bus connection string is invalid.", e);
                exception.Data[nameof(connectionString)] = connectionString;
                throw exception;
            }
        }
    }
}
