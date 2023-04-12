using MassTransit;

namespace MTPerformance.Contracts
{
    public interface IEndpointAddressProvider
    {
        Uri GetEndpoint<T>();
    }

    public class MemoryEndpointAddressProvider : IEndpointAddressProvider
    {
        private readonly IEndpointNameFormatter _formatter;

        public MemoryEndpointAddressProvider(IEndpointNameFormatter formatter)
        {
            _formatter = formatter;
        }

        public Uri GetEndpoint<T>()
        {
            var name = typeof(T).Name;
            var sanitizeName = _formatter.SanitizeName(name);

            return new Uri($"queue:{sanitizeName}");
        }
    }

    public class RabbitMqEndpointAddressProvider : IEndpointAddressProvider
    {
        private readonly IEndpointNameFormatter _formatter;

        public RabbitMqEndpointAddressProvider(IEndpointNameFormatter formatter)
        {
            _formatter = formatter;
        }

        public Uri GetEndpoint<T>()
        {
            var name = typeof(T).Name;
            var sanitizeName = _formatter.SanitizeName(name);

            return new Uri($"queue:{sanitizeName}");
        }
    }
}
