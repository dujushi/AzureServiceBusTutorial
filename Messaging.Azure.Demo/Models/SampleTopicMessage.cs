using System;
using Messaging.Attributes;

namespace Messaging.Azure.Demo.Models
{
    [Topic("UserChanged")]
    public class SampleTopicMessage
    {
        public int SampleId { get; set; }
        public DateTime WhenCreated { get; set; }
    }
}
