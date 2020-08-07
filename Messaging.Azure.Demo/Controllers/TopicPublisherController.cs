using System;
using System.Threading.Tasks;
using Messaging.Azure.Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Azure.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopicPublisherController : ControllerBase
    {
        private readonly ITopicPublisher<SampleTopicMessage> _sampleTopicMessagePublisher;
        private readonly ITopicPublisher<Sample1DayTopicMessage> _sample1DayTopicMessagePublisher;

        public TopicPublisherController(
            ITopicPublisher<SampleTopicMessage> sampleTopicMessagePublisher,
            ITopicPublisher<Sample1DayTopicMessage> sample1DayTopicMessagePublisher)
        {
            _sampleTopicMessagePublisher = sampleTopicMessagePublisher ?? throw new ArgumentNullException(nameof(sampleTopicMessagePublisher));
            _sample1DayTopicMessagePublisher = sample1DayTopicMessagePublisher ?? throw new ArgumentNullException(nameof(sample1DayTopicMessagePublisher));
        }

        [HttpGet]
        public async Task GetAsync()
        {
            await _sampleTopicMessagePublisher.SendAsync(new SampleTopicMessage
            {
                SampleId = 1,
                WhenCreated = DateTime.Now
            });

            await _sample1DayTopicMessagePublisher.SendAsync(new Sample1DayTopicMessage());
        }
    }
}
