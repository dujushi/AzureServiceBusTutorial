using System;
using Messaging.Azure.Attributes;

namespace Messaging.Azure.Demo.Models
{
    [ServiceBusTopic("test")]
    public class SampleTopicMessage
    {
        public int SampleId { get; set; }
        public DateTime WhenCreated { get; set; }
    }
}
