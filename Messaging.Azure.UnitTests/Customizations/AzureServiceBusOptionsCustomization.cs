using AutoFixture;
using Messaging.Azure.Models;

namespace Messaging.Azure.UnitTests.Customizations
{
    public class AzureServiceBusOptionsCustomization : ICustomization
    {
        private readonly string _connectionString;

        public AzureServiceBusOptionsCustomization(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Register(() => new AzureServiceBusOptions
            {
                ConnectionString = _connectionString
            });
        }
    }
}
