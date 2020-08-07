using System;

namespace Messaging.Azure.Models
{
    /// <summary>
    /// Represents the context of the current operation.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Gets the unique identifier of the current tenant.
        /// </summary>
        public string TenantId { get; }

        /// <summary>
        /// Gets the unique identifier of the current user.
        /// </summary>
        public Guid? UserId { get; }

        /// <summary>
        /// Gets the correlation id.
        /// </summary>
        public string CorrelationId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="tenantId">The unique identifier of the current tenant</param>
        /// <param name="userId">The unique identifier of the current user</param>
        /// <param name="correlationId">The correlation id</param>
        public Context(string tenantId, Guid? userId, string correlationId)
        {
            TenantId = tenantId;
            UserId = userId;
            CorrelationId = correlationId;
        }
    }
}
