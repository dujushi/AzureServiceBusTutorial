using Messaging.Attributes;

namespace Messaging.Azure.Demo.Models
{
    [Topic("UserChanged", 1)]
    public class Sample1DayTopicMessage
    {
    }
}
