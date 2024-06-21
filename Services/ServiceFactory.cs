namespace MovieTicketAPI.Services
{
    public enum ServiceType
    {
        Movie,
        Ticket,
        User
    }

    public class ServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object CreateService(ServiceType serviceType)
        {
            return serviceType switch
            {
                ServiceType.Movie => _serviceProvider.GetRequiredService<MovieService>(),
                ServiceType.Ticket => _serviceProvider.GetRequiredService<TicketService>(),
                ServiceType.User => _serviceProvider.GetRequiredService<UserService>(),
                _ => throw new ArgumentOutOfRangeException(nameof(serviceType), serviceType, null)
            };
        }
    }
}
