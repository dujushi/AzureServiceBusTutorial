using System;
using System.Reflection;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Messaging.Azure.Attributes;
using Messaging.Azure.UnitTests.Models;
using Messaging.Exceptions;
using Microsoft.Azure.ServiceBus;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using Xunit;

namespace Messaging.Azure.UnitTests
{
    public class AzureTopicPublisherTests
    {
        [Theory, AutoMockData]
        public void AzureTopicPublisher_WithMessageWithoutAttribute_ThrowsMessagingException(IFixture fixture)
        {
            Action action = () => fixture.Create<AzureTopicPublisher<MessageWithoutAttribute>>();

            action.Should().Throw<ObjectCreationException>()
                .WithInnerException<TargetInvocationException>()
                .WithInnerException<MessagingException>()
                .WithMessage($"Topic message should be decorated with the {nameof(ServiceBusTopicAttribute)}.");
        }

        [Theory, AutoMockData]
        public void SendAsync_WithMessageWithEmptyTopicName_ThrowsMessagingException(
            IFixture fixture)
        {
            Action action = () => fixture.Create<AzureTopicPublisher<MessageWithEmptyTopicName>>();

            action.Should().Throw<ObjectCreationException>()
                .WithInnerException<TargetInvocationException>()
                .WithInnerException<MessagingException>()
                .WithMessage("The topic name cannot be empty.");
        }

        [Theory, AutoMockData]
        public void SendAsync_WithUnauthorizedException_ThrowsMessagingException(
            [Frozen]ITopicClient topicClient,
            AzureTopicPublisher<MessageWithValidAttribute> sut)
        {
            Mock.Get(topicClient)
                .Setup(x => x.SendAsync(It.IsAny<Message>()))
                .ThrowsAsync(new UnauthorizedException(""));

            Func<Task> func = () => sut.SendAsync(new MessageWithValidAttribute());

            func.Should().Throw<MessagingException>()
                .WithMessage("The Azure Service Bus connection string is invalid.");
        }

        [Theory, AutoMockData]
        public void SendAsync_WithMessagingEntityNotFoundException_ThrowsMessagingException(
            [Frozen]ITopicClient topicClient,
            AzureTopicPublisher<MessageWithValidAttribute> sut)
        {
            Mock.Get(topicClient)
                .Setup(x => x.SendAsync(It.IsAny<Message>()))
                .ThrowsAsync(new MessagingEntityNotFoundException(""));

            Func<Task> func = () => sut.SendAsync(new MessageWithValidAttribute());

            func.Should().Throw<MessagingException>()
                .WithMessage("The topic doesn't exist.");
        }

        [Theory, AutoMockData]
        public void SendAsync_AllGood(AzureTopicPublisher<MessageWithValidAttribute> sut)
        {
            Func<Task> func = () => sut.SendAsync(new MessageWithValidAttribute());

            func.Should().NotThrow();
        }
    }
}
