using Kosinka.Core.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kosinka.Attributes
{
    enum KindOfRegistration
    {
        Singleton = 0,
        Scoped = 1,
        Transient = 2,
    }

    public class ServiceHelper
    {
        protected static IServiceCollection ServiceCollection { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }

        static ServiceHelper()
        {
            ServiceCollection = new ServiceCollection();
            var allUseTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x=>x.GetCustomAttribute(typeof(UseInAppAttribute)) is not null);
            foreach (var type in allUseTypes)
            {
                var kind = type.GetCustomAttribute<UseInAppAttribute>()?.Kind;
                var interfaces = type.GetInterfaces();
                switch (kind)
                {
                    case KindOfRegistration.Singleton:
                        foreach(var _interface in interfaces)
                        {
                            ServiceCollection.AddSingleton(_interface, type);
                        }
                        break;
                    case KindOfRegistration.Scoped:
                        foreach (var _interface in interfaces)
                        {
                            ServiceCollection.AddScoped(_interface,type);
                        }
                        break;
                    case KindOfRegistration.Transient:
                        foreach (var _interface in interfaces)
                        {
                            ServiceCollection.AddTransient(_interface,type);
                        }
                        break;
                    default:
                        throw new InvalidCastException("Impossible");
                }
            }
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }
    }

    internal class UseInAppAttribute : Attribute 
    {
        public KindOfRegistration Kind { get; init; }
        public UseInAppAttribute(KindOfRegistration kind)
        {
            Kind = kind;
        }
    }
}
