using System.Threading.Tasks;

namespace Messaging
{
    /// <summary>
    /// Represents an object to send message to a topic queue.
    /// </summary>
    /// <typeparam name="T">The type of the message</typeparam>
    public interface ITopicPublisher<in T>
    {
        /// <summary>
        /// Sends a message to a topic queue.
        /// </summary>
        /// <param name="topicMessage">The message</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendAsync(T topicMessage);
    }
}
