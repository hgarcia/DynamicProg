using System;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace DynamicProg.DI
{
    public static class IocContainer
    {
        private static readonly IWindsorContainer _container;

        static IocContainer()
        {
            _container = new WindsorContainer(new XmlInterpreter());   
        }

		public static T GetClassInstance<T>()
		{
			return _container.Resolve<T>();
		}

		public static T GetClassInstance<T>(string key)
		{
			return _container.Resolve<T>(key);
		}

        public static object GetClassInstance(string key)
        {
            return _container.Resolve(key);
        }

        public static void RegisterTransient<TFacility>(string key, Type implementation)
        {
            _container.AddComponentWithLifestyle(key,typeof(TFacility),implementation,LifestyleType.Transient);
        }
    }
}