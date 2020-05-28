using StructureMap.DynamicInterception;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using CacheLayer.Attributes;

namespace CacheLayer.Entities
{
    public class CachingInterceptor : ISyncInterceptionBehavior
    {
        public IMethodInvocationResult Intercept(ISyncMethodInvocation methodInvocation)
        {
            IMethodInvocationResult result = null;

            CacheAttribute attribute = methodInvocation.InstanceMethodInfo.GetCustomAttribute<CacheAttribute>();
            if (attribute != null)
            {
                string key = KeyBuilder.BuildCacheKey(attribute.CacheSetting, attribute.CacheName, methodInvocation.TargetInstance.ToString(), methodInvocation.InstanceMethodInfo.Name, attribute.PropertyName, methodInvocation.Arguments.ToArray());
                string area = KeyBuilder.BuildCacheKey(attribute.CacheSetting, attribute.CacheArea, methodInvocation.TargetInstance.ToString(), methodInvocation.InstanceMethodInfo.Name, attribute.PropertyName, methodInvocation.Arguments.ToArray());
                
                CachedItemDTO cachedItem = CacheManager.GetValue(area, key);

                object cachedValue = null;

                if (cachedItem != null)
                {
                    cachedValue = cachedItem.CacheValue;
                }

                if (cachedValue != null)
                {
                    MethodInfo methodInfo = methodInvocation.InstanceMethodInfo;
                    Type typeFoo = methodInfo.ReturnType;

                    var returnData = Convert.ChangeType(cachedValue, typeFoo);

                    return methodInvocation.CreateResult(returnData);
                }
                else
                {
                    result = methodInvocation.InvokeNext();

                    CacheManager.AddValue(area, key, result.ReturnValue, attribute.TimeoutSeconds);

                    return result;
                }
            }
            else
            {
                result = methodInvocation.InvokeNext();
                return result;
            }
        }
    }
}


//public class AsyncCachingInterceptor : IAsyncInterceptionBehavior
//{
//    private static readonly IDictionary<int, int> PrecalculatedValues = new Dictionary<int, int>
//        {
//            { 16, 4444 },
//            { 10, 5555 },
//        };

//    public async Task<IMethodInvocationResult> InterceptAsync(IAsyncMethodInvocation methodInvocation)
//    {
//        var argument = methodInvocation.GetArgument("value");
//        var argumentValue = (int)argument.Value;

//        int result;
//        return PrecalculatedValues.TryGetValue(argumentValue, out result)
//            ? methodInvocation.CreateResult(result)
//            : await methodInvocation.InvokeNextAsync().ConfigureAwait(false);
//    }
//}

//public class NegatingInterceptor : ISyncInterceptionBehavior
//{
//    public IMethodInvocationResult Intercept(ISyncMethodInvocation methodInvocation)
//    {
//        var argument = methodInvocation.GetArgument("value");
//        var argumentValue = (int)argument.Value;
//        if (argumentValue < 0)
//        {
//            argument.Value = -argumentValue;
//        }

//        IMethodInvocationResult result = methodInvocation.InvokeNext();

//        return result;
//    }
//}
