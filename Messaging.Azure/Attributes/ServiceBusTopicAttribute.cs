using System;
using Messaging.Exceptions;

namespace Messaging.Azure.Attributes
{
    /// <summary>
    /// Configures topic name and time to live value for the message
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceBusTopicAttribute : Attribute
    {
        /// <summary>
        /// Gets the Azure Service Bus topic name to send this message to
        /// </summary>
        public string TopicName { get; }

        /// <summary>
        /// Gets the time to live value for this message
        /// </summary>
        public TimeSpan TimeToLive { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusTopicAttribute"/> class.
        /// Configures topic name for this message with default time to live value of 7 days
        /// </summary>
        /// <param name="topicName">The Azure Service Bus topic name to send this message to</param>
        public ServiceBusTopicAttribute(string topicName)
            : this(topicName, 7)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusTopicAttribute"/> class.
        /// Configures topic name and time to live value in days for the message
        /// </summary>
        /// <param name="topicName">The Azure Service Bus topic name to send this message to</param>
        /// <param name="timeToLiveInDays">The time to live value in days</param>
        public ServiceBusTopicAttribute(string topicName, int timeToLiveInDays)
            : this(topicName, TimeSpan.FromDays(timeToLiveInDays))
        {
        }

        private ServiceBusTopicAttribute(string topicName, TimeSpan timeToLive)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new MessagingException("The topic name cannot be empty.");
            }

            TopicName = topicName;
            TimeToLive = timeToLive;
        }
    }
}
