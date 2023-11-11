using System;
using System.Reflection;

namespace MazeChallenge.API.Installers
{
    public static class RegisterInterfacesExtensions
    {

        public static void RegisterAllDirectImplementations<T>(this IServiceCollection services, ServiceLifetime lifetime, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                assemblies = new[] { Assembly.GetAssembly(typeof(T)) };
            }

            services.RegisterAssemblyPublicNonGenericClasses(assemblies)
                .Where(x => typeof(T).IsAssignableFrom(x))
                .OnlyDerivedImplementations(lifetime);
        }

        public static AutoRegisterData RegisterAssemblyPublicNonGenericClasses(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                assemblies = new[] { Assembly.GetCallingAssembly() };
            }

            var allPublicTypes = assemblies.SelectMany(x =>
                x.GetExportedTypes().Where(y => y.IsClass && !y.IsAbstract && !y.IsGenericType && !y.IsNested)
            );
            return new AutoRegisterData(services, allPublicTypes);
        }

        public static AutoRegisterData Where(this AutoRegisterData autoRegisterData, Func<Type, bool> predicate)
        {
            if (autoRegisterData == null) throw new ArgumentNullException(nameof(autoRegisterData));
            autoRegisterData.TypeFilter = predicate;
            return new AutoRegisterData(autoRegisterData.Services, autoRegisterData.TypesToConsider.Where(predicate));
        }

        public static IServiceCollection OnlyDerivedImplementations(this AutoRegisterData autoRegisterData, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (autoRegisterData == null) throw new ArgumentNullException(nameof(autoRegisterData));

            foreach (var classType in (autoRegisterData.TypeFilter == null
                ? autoRegisterData.TypesToConsider
                : autoRegisterData.TypesToConsider.Where(autoRegisterData.TypeFilter)))
            {
                var interfaces = classType.GetTypeInfo().GetInterfaces();
                foreach (var infc in interfaces.Where(i => i != typeof(IDisposable) && i.IsPublic && !i.IsNested))
                {
                    if (!interfaces.Any(i => i.GetInterfaces().Contains(infc)))
                    {
                        autoRegisterData.Services.Add(new ServiceDescriptor(infc, classType, lifetime));
                    }
                }
            }

            return autoRegisterData.Services;
        }

        public static IServiceCollection AsPublicImplementedInterfaces(this AutoRegisterData autoRegisterData, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (autoRegisterData == null) throw new ArgumentNullException(nameof(autoRegisterData));

            foreach (var classType in (autoRegisterData.TypeFilter == null
                ? autoRegisterData.TypesToConsider
                : autoRegisterData.TypesToConsider.Where(autoRegisterData.TypeFilter)))
            {
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces;
                foreach (var infc in interfaces.Where(i => i != typeof(IDisposable) && i.IsPublic && !i.IsNested))
                {
                    if (!interfaces.Any(i => i.GetInterfaces().Contains(infc)))
                    {
                        autoRegisterData.Services.Add(new ServiceDescriptor(infc, classType, lifetime));
                    }
                }
            }

            return autoRegisterData.Services;
        }

        public class AutoRegisterData
        {
            public AutoRegisterData(IServiceCollection services, IEnumerable<Type> typesToConsider)
            {
                Services = services ?? throw new ArgumentNullException(nameof(services));
                TypesToConsider = typesToConsider ?? throw new ArgumentNullException(nameof(typesToConsider));
            }

            public IServiceCollection Services { get; set; }
            public IEnumerable<Type> TypesToConsider { get; set; }
            public Func<Type, bool> TypeFilter { get; set; }
        }
    }
}

