using System.Linq;
using Domain.Services;
using Infrastructure.DataProvider;
using Infrastructure.DataProvider.Caching;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Filters;

namespace WebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositoriesAndServices(this IServiceCollection serviceCollection,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var repositoryAssembly = typeof(ApplicationContext).Assembly;

            foreach (var type in repositoryAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.Namespace == "Infrastructure.DataProvider.Repositories" ||
                            t.Namespace == "Infrastructure.DataProvider.Services" ||
                            t.Namespace == "Integration.Services"))
            {
                foreach (var i in type.GetInterfaces())
                {
                    serviceCollection.Add(new ServiceDescriptor(i.IsGenericType
                        ? i.GetGenericTypeDefinition().MakeGenericType(i.GetGenericArguments())
                        : i, type, lifetime));
                }
            }

            serviceCollection.AddScoped(typeof(ICacheService<,>), typeof(RedisService<,>));
            serviceCollection.AddScoped(typeof(UnitOfWork));
        }

        public static void AddTransactionPerRequestFilter(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(TransactionPerRequestActionFilterAttribute),
                typeof(TransactionPerRequestActionFilterAttribute));
            serviceCollection.AddMvc(setup => setup.Filters.AddService<TransactionPerRequestActionFilterAttribute>(1));
        }
    }
}