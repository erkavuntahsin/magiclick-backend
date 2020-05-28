using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StructureMap.DynamicInterception;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Core.Logger
{
    public class LogInterceptor : ISyncInterceptionBehavior
    {
        ILogger<LogInterceptor> _logger;
        public LogInterceptor(ILogger<LogInterceptor> logger)
        {
            _logger = logger;
        }

        public IMethodInvocationResult Intercept(ISyncMethodInvocation methodInvocation)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IMethodInvocationResult result = null;
            MethodInfo methodInfo = methodInvocation.InstanceMethodInfo;
            Type typeFoo = methodInfo.ReturnType;
            result = methodInvocation.InvokeNext();
            stopwatch.Stop();

            Dictionary<string, string> requestDictionary = new Dictionary<string, string>();

            foreach (IArgument argument in methodInvocation.Arguments)
            {
                requestDictionary.Add(argument.ParameterInfo.Name, JsonConvert.SerializeObject(argument.Value, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            try
            {
                string request = JsonConvert.SerializeObject(requestDictionary);
                string response = JsonConvert.SerializeObject(result.ReturnValue, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                _logger.LogInformation($"LogInterceptor logging process succesfully, method name : { methodInfo.DeclaringType.FullName}-{methodInfo.Name} | Request : {request} | Response : {response} | Total Processing Time  : {stopwatch.ElapsedMilliseconds}");
                PropertyInfo pi = result.ReturnValue.GetType().GetProperty("IsSuccess");

                bool isSuccess = (bool)(pi.GetValue(result.ReturnValue, null));
                if (isSuccess)
                {
                    // _logger.LogInformation($"LogInterceptor logging process succesfully, method name : { methodInfo.DeclaringType.FullName}-{methodInfo.Name} | Request : {request} | Response : {response} | Total Processing Time  : {stopwatch.ElapsedMilliseconds}");
                }
                else
                    _logger.LogError($"LogInterceptor logging process succesfully, method name : { methodInfo.DeclaringType.FullName}-{methodInfo.Name} | Request : {request} | Response : {response} | Total Processing Time  : {stopwatch.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
            }
            return result;
        }

    }
}
