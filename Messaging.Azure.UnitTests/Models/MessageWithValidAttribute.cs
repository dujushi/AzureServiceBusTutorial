using Messaging.Azure.Attributes;

namespace Messaging.Azure.UnitTests.Models
{
    [ServiceBusTopic("ValidTopicName")]
    public class MessageWithValidAttribute
    {
    }
}
