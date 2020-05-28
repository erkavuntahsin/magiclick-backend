using StructureMap;
using StructureMap.Pipeline;
using StructureMap.DynamicInterception;
using System;
using System.Collections.Generic;
using System.Text;

namespace CacheLayer.Entities
{

    public class CacheServiceRegistry<TPluginType, TConcreteType> : Registry
        where TConcreteType : TPluginType
        where TPluginType : class
    {
        public CacheServiceRegistry(ILifecycle lifecycle = null)
        {
            For<TPluginType>(lifecycle).Use<TConcreteType>().Singleton().AddInterceptor(new DynamicProxyInterceptor<TPluginType>(new IInterceptionBehavior[]
                    {
                        new CachingInterceptor(),
                        new CacheInvalidateInterceptor(),
                    }));
        }

        public CacheServiceRegistry(SmartInstance<TConcreteType, TPluginType> smartInstance)
        {
            smartInstance.AddInterceptor(new DynamicProxyInterceptor<TPluginType>(new IInterceptionBehavior[]
                    {
                        new CachingInterceptor(),
                        new CacheInvalidateInterceptor(),
                    }));

        }
    }
}
