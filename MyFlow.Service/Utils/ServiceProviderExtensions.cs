using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Service.Utils
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider, string invokeClassName) where T : class
        {
            try 
            {
                IList<T> services = serviceProvider.GetServices(typeof(T)).Cast<T>().ToList();

                var result = services.Where(o => o.GetType().Name == invokeClassName)
                                .First();

                return result;
            }
            catch (Exception) 
            {
                throw new Exception($"Not Found {invokeClassName}");
            }
        }
    }
}
