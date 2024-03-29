﻿using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace MyFlow.Service.Aop
{
    public class LoggerAttribute : AbstractInterceptorAttribute
    {
        [FromServiceContext]
        public ILogger<LoggerAttribute>? Logger { get; set; }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
        
            try
            {
                Logger!.LogDebug($"Method Name :{context.ServiceMethod} Begin!!");
                await next(context);
                Logger!.LogDebug($"Method Name :{context.ServiceMethod} End, Return : {context.ReturnValue}");
            }
            catch (Exception ex)
            {
                Logger!.LogError(ex.ToString());  // 記錄例外錯誤...
                throw;
            }
        }
    }
}
