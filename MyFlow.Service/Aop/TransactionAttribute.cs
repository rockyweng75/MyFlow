using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace MyFlow.Service.Aop
{
    public class TransactionAttribute : AbstractInterceptorAttribute
    {
        [FromServiceContext]
        public ILogger<TransactionAttribute>? Logger { get; set; }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    await next(context);
                    ts.Complete();  
                }
                catch (Exception ex)
                {
                    Logger!.LogError(ex.ToString());  // 記錄例外錯誤...
                    throw;
                }
            }
        }
    }
}
