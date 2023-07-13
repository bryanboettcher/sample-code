using Microsoft.Extensions.DependencyInjection;

namespace DiyAutoScanner.App;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ScanAssembly<TAnchor>(this IServiceCollection services)
    {
        var assembly = typeof(TAnchor).Assembly;

        foreach (var type in assembly.GetTypes())
        {
            // skip things we can't instantiate anyway
            if (type.IsNotPublic)
                continue;
            if (type.IsAbstract)
                continue;
            
            // types we probably don't want to instantiate
            if (!type.IsClass)
                continue;

            var interfaces = type.GetInterfaces();
            
            // nothing to bolt together anyway
            if (interfaces.Length == 0)
                continue;

            // special case where we may have open-generic class and open-generic interface
            if (type.IsGenericType && !type.IsConstructedGenericType)
            {
                foreach (var candidate in interfaces)
                {
                    if (!candidate.IsGenericType)
                        continue;

                    var typeArguments = candidate.GenericTypeArguments;
                    
                    // this code isn't smart enough to map different number of generics together
                    if (typeArguments.Length != type.GetGenericArguments().Length)
                        continue;

                    services.Add(ServiceDescriptor.Singleton(candidate, type));
                }

                continue;
            }

            foreach (var candidate in interfaces)
            {
                // just get rid of things we probably don't want to map
                if (candidate.Namespace?.StartsWith("System") ?? false)
                    continue;
                if (candidate.Namespace?.StartsWith("Microsoft") ?? false)
                    continue;
                
                services.Add(ServiceDescriptor.Singleton(candidate, type));
            }
        }

        return services;
    }
}