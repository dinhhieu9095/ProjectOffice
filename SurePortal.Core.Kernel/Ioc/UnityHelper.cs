using AutoMapper;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Mapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace DaiPhatDat.Core.Kernel.Ioc
{
    public static class UnityHelper
    {
        public static IUnityContainer BuildUnityContainer(IUnityContainer container)
        {
            // special case for factory method
            container.RegisterType<IDbContextFactory, DefaultDbContextFactory>(new PerResolveLifetimeManager());

            // register all your components with the container
            // it is NOT necessary to register controllers here
            // prefer using conventions as much as possible
            var types = AllClasses
                .FromLoadedAssemblies()
                .Where(predicate => predicate.Namespace != null
                                    && predicate.Namespace.StartsWith("SurePortal")
                                    && !string.IsNullOrEmpty(predicate.AssemblyQualifiedName)
                                    && !typeof(Controller).IsAssignableFrom(predicate)
                                    && !predicate.IsAbstract
                                    && !predicate.IsInterface)
                .ToList();


            #region HEADER INTERFACES AND CONCRETES IMPLEMENT

            container.RegisterTypes(types,
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.PerResolve);

            #endregion HEADER INTERFACES AND CONCRETES IMPLEMENT

            #region AUTOMAPPER

            MappingRegistration(container, types);

            #endregion AUTOMAPPER

            return container;
        }

        private static void MappingRegistration(IUnityContainer container, IList<System.Type> types)
        {
            try
            {
                var maps = types
                    .SelectMany(collectionSelector => collectionSelector
                            .GetInterfaces()
                            .Where(predicate =>
                                predicate.IsGenericType && predicate.GetGenericTypeDefinition() == typeof(IMapping<>)),
                        (type, itype) => new
                        {
                            Source = itype.GetGenericArguments().First(),
                            Destination = type
                        })
                    .ToArray();

                var complexMaps = types
                    .SelectMany(collectionSelector => collectionSelector
                            .GetInterfaces()
                            .Where(predicate => typeof(IComplexMapping).IsAssignableFrom(predicate)),
                        (type, itype) => Activator.CreateInstance(type) as IComplexMapping)
                    .ToArray();

                container
                    .RegisterType<MapperConfiguration>(
                        new ContainerControlledLifetimeManager(),
                        new InjectionFactory(factoryFunc =>
                            new MapperConfiguration(configure =>
                            {
                                configure.ConstructServicesUsing(constructor => factoryFunc.Resolve(constructor));

                                foreach (var map in maps)
                                    configure
                                        .CreateMap(map.Source, map.Destination)
                                        .ReverseMap();

                                foreach (var map in complexMaps) map.CreateMap(configure);
                            })))
                    .RegisterType<IConfigurationProvider>(
                        new ContainerControlledLifetimeManager(),
                        new InjectionFactory(factoryFunc => factoryFunc.Resolve<MapperConfiguration>()))
                    .RegisterType<IMapper>(
                        new ContainerControlledLifetimeManager(),
                        new InjectionFactory(factoryFunc => factoryFunc
                            .Resolve<MapperConfiguration>()
                            .CreateMapper()));
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                var stringBuilder = new StringBuilder();

                foreach (var loaderException in reflectionTypeLoadException.LoaderExceptions)
                {
                    stringBuilder.AppendLine(loaderException.Message);

                    if (loaderException is FileNotFoundException fileNotFoundException)
                        if (!string.IsNullOrEmpty(fileNotFoundException.FusionLog))
                            stringBuilder.AppendLine(fileNotFoundException.FusionLog);

                    stringBuilder.AppendLine();
                }

                Debug.Write(stringBuilder.ToString());
            }
        }
    }
}