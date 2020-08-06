using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Messaging.Exceptions
{
    /// <summary>
    /// Represents exceptions related to messaging services.
    /// </summary>
    [Serializable]
    public class MessagingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingException"/> class.
        /// </summary>
        public MessagingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingException"/> class.
        /// </summary>
        /// <param name="message">The exception message</param>
        public MessagingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingException"/> class.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception</param>
        public MessagingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingException"/> class.
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected MessagingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
