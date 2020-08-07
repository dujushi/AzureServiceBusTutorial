using System;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Messaging.Azure.Attributes;
using Messaging.Azure.Interfaces;
using Messaging.Azure.Models;
using Messaging.Exceptions;
using Microsoft.Azure.ServiceBus;

namespace Messaging.Azure
{
    /// <inheritdoc />
    public class AzureTopicPublisher<T> : ITopicPublisher<T>
    {
        private readonly ITopicClient _topicClient;
        private readonly TimeSpan _timeToLive;
        private readonly Context _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTopicPublisher{T}"/> class.
        /// </summary>
        /// <param name="context">The context</param>
        /// <param name="topicClientProvider">The topic client provider</param>
        public AzureTopicPublisher(Context context, ITopicClientProvider topicClientProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            if (topicClientProvider == null)
            {
                throw new ArgumentNullException(nameof(topicClientProvider));
            }

            var attribute = typeof(T).GetCustomAttribute<ServiceBusTopicAttribute>();
            if (attribute == null)
            {
                throw new MessagingException($"Topic message should be decorated with the {nameof(ServiceBusTopicAttribute)}.");
            }

            _topicClient = topicClientProvider.Get(attribute.TopicName);
            _timeToLive = attribute.TimeToLive;
        }

        /// <inheritdoc/>
        public async Task SendAsync(T topicMessage)
        {
            var topicMessageString = JsonSerializer.Serialize(topicMessage);
            var topicMessageBytes = Encoding.UTF8.GetBytes(topicMessageString);
            var message = new Message(topicMessageBytes);
            message.UserProperties.Add(nameof(_context.TenantId), _context.TenantId);
            message.UserProperties.Add(nameof(_context.UserId), _context.UserId);
            message.CorrelationId = _context.CorrelationId;
            message.TimeToLive = _timeToLive;
            try
            {
                await _topicClient.SendAsync(message);
            }
            catch (UnauthorizedException e)
            {
                throw new MessagingException("The Azure Service Bus connection string is invalid.", e);
            }
            catch (MessagingEntityNotFoundException e)
            {
                var exception = new MessagingException("The topic doesn't exist.", e);
                exception.Data[nameof(_topicClient.TopicName)] = _topicClient.TopicName;
                throw exception;
            }
        }
    }
}
