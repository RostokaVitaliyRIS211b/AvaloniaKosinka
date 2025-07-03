using Kosinka.Core.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
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

    internal class UseInAppAttribute : Attribute
    {
        protected static IServiceCollection Services { get; set; } = new ServiceCollection();

        public static IServiceProvider ServiceProvider { get => Services.BuildServiceProvider(); }

        public UseInAppAttribute(Type type, KindOfRegistration kind)
        {
            switch (kind)
            {
                case KindOfRegistration.Singleton:
                    Services.AddSingleton(type);
                    break;
                case KindOfRegistration.Scoped:
                    Services.AddScoped(type);
                    break;
                case KindOfRegistration.Transient:
                    Services.AddTransient(type);
                    break;
            }
        }
    }
}
