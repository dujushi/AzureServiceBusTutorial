using System;
using System.Reflection;
using AutoFixture;
using FluentAssertions;
using Messaging.Azure.Models;
using Messaging.Azure.Providers;
using Messaging.Azure.UnitTests.Customizations;
using Messaging.Exceptions;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using Objectivity.AutoFixture.XUnit2.Core.Attributes;
using Xunit;

namespace Messaging.Azure.UnitTests.Providers
{
    public class TopicClientProviderTests
    {
        private const string EmptyConnectionString = "";
        private const string InvalidConnectionString = nameof(InvalidConnectionString);
        private const string ValidConnectionString = "Endpoint=sb://test.servicebus.windows.net/;SharedAccessKeyName=test;SharedAccessKey=test";

        [Theory, AutoMockData]
        public void TopicClientProvider_WhenMissingConnectionString_ThrowsMessagingException(
            [CustomizeWith(typeof(AzureServiceBusOptionsCustomization), EmptyConnectionString)]AzureServiceBusOptions options,
            IFixture fixture)
        {
            Action action = () => fixture.Create<TopicClientProvider>();

            action.Should().Throw<ObjectCreationException>()
                .WithInnerException<TargetInvocationException>()
                .WithInnerException<MessagingException>()
                .WithMessage("Azure Service Bus connection string is not configured.");
        }

        [Theory, AutoMockData]
        public void TopicClientProvider_WithInvalidConnectionString_ThrowsMessagingException(
            [CustomizeWith(typeof(AzureServiceBusOptionsCustomization), InvalidConnectionString)]AzureServiceBusOptions options,
            IFixture fixture)
        {
            Action action = () => fixture.Create<TopicClientProvider>();

            action.Should().Throw<ObjectCreationException>()
                .WithInnerException<TargetInvocationException>()
                .WithInnerException<MessagingException>()
                .WithMessage("The provided Azure Service Bus connection string is invalid.");
        }

        [Theory, AutoMockData]
        public void Get_WithValidConnectionString_ReturnsTopicClient(
            [CustomizeWith(typeof(AzureServiceBusOptionsCustomization), ValidConnectionString)]AzureServiceBusOptions options,
            string topicName,
            TopicClientProvider sut)
        {

            var topicClient = sut.Get(topicName);

            topicClient.TopicName.Should().Be(topicName);
        }
    }
}
