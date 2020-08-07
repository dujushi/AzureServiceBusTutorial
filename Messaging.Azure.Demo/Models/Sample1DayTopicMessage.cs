using Messaging.Azure.Attributes;

namespace Messaging.Azure.Demo.Models
{
    [ServiceBusTopic("test", 1)]
    public class Sample1DayTopicMessage
    {
    }
}
