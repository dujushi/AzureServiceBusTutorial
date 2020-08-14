using System;
using Messaging.Exceptions;

namespace Messaging.Attributes
{
    /// <summary>
    /// Configures topic name and time to live value for the message
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TopicAttribute : Attribute
    {
        /// <summary>
        /// Gets the topic name to send this message to
        /// </summary>
        public string TopicName { get; }

        /// <summary>
        /// Gets the time to live value for this message
        /// </summary>
        public TimeSpan TimeToLive { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicAttribute"/> class.
        /// Configures topic name for this message with default time to live value of 7 days
        /// </summary>
        /// <param name="topicName">The topic name to send this message to</param>
        public TopicAttribute(string topicName)
            : this(topicName, 7)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicAttribute"/> class.
        /// Configures topic name and time to live value in days for the message
        /// </summary>
        /// <param name="topicName">The topic name to send this message to</param>
        /// <param name="timeToLiveInDays">The time to live value in days</param>
        public TopicAttribute(string topicName, int timeToLiveInDays)
            : this(topicName, TimeSpan.FromDays(timeToLiveInDays))
        {
        }

        private TopicAttribute(string topicName, TimeSpan timeToLive)
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
