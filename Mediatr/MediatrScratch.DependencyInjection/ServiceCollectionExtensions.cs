using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatrFromScratch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatrScratch.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, ServiceLifetime serviceLifetime,
            params Type[] markers)
        {
            var handlerInfo = new Dictionary<Type, Type>();

            foreach (var marker in markers)
            {
                var assembly = marker.Assembly;
                var requests = GetClassesImplementInterfaces(assembly, typeof(IRequest<>));
                var handlers = GetClassesImplementInterfaces(assembly, typeof(IHandler<,>));
                requests.ForEach(x =>
                {
                    handlerInfo[x] =
                        handlers.SingleOrDefault(xx => x == xx.GetInterface("IHandler`2")!.GetGenericArguments()[0]);
                });
                var serviceDescriptors = handlers.Select(x => new ServiceDescriptor(x, x, serviceLifetime));
                services.TryAdd(serviceDescriptors);
            }

            services.AddSingleton<IMediator>(x => new Mediator(x.GetRequiredService, handlerInfo));
            return services;
        }

        private static List<Type> GetClassesImplementInterfaces(Assembly assembly, Type typeToMatch)
        {
            return assembly.ExportedTypes
                .Where(type =>
                {
                    var genericInterfaceTypes = type.GetInterfaces().Where(x => x.IsGenericType).ToList();
                    var implementRequestType = genericInterfaceTypes
                        .Any(x => x.GetGenericTypeDefinition() == typeToMatch);
                    return !type.IsInterface && !type.IsAbstract && implementRequestType;
                }).ToList();
        }
    }
}