using StructureMap.DynamicInterception;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using CacheLayer.Attributes;

namespace CacheLayer.Entities
{
    public class CacheInvalidateInterceptor : ISyncInterceptionBehavior
    {
        public IMethodInvocationResult Intercept(ISyncMethodInvocation methodInvocation)
        {
            IEnumerable<CacheInvalidateAttribute> invalidateAttributeList = methodInvocation.InstanceMethodInfo.GetCustomAttributes<CacheInvalidateAttribute>();
            foreach (CacheInvalidateAttribute attribute in invalidateAttributeList)
            {
                //string key = KeyBuilder.BuildKey(attribute.CacheSetting, attribute.CacheName, attribute.PropertyName, methodInvocation.Arguments.ToArray());
                string area = KeyBuilder.BuildInvalidateCacheKey(attribute.CacheSetting, attribute.CacheArea, attribute.PropertyName, methodInvocation.Arguments.ToArray());

                //CachedItemDTO cachedItem = CacheManager.GetValue(attribute.CacheArea, key);

                //if (cachedItem != null)
                //{
                CacheManager.RemoveCacheArea(ca => ca._CacheAreaName.StartsWith(area));
                //}
            }

            return methodInvocation.InvokeNext();
        }
    }
}
