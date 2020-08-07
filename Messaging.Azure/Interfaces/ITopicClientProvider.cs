using Microsoft.Azure.ServiceBus;

namespace Messaging.Azure.Interfaces
{
    /// <summary>
    /// Represents the topic client provider.
    /// </summary>
    public interface ITopicClientProvider
    {
        /// <summary>
        /// Returns the topic client by the provided topic name.
        /// </summary>
        /// <param name="topicName">The topic name</param>
        /// <returns>The topic client</returns>
        ITopicClient Get(string topicName);
    }
}
