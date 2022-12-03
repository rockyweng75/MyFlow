using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using MyFlow.Service;
using MyFlow.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MyFlow.Test
{
    public class BasicTestServer : IDisposable
    {
        protected WebApplicationFactory<Program>? application;
        private bool disposedValue;

        public IServiceProvider services => application!.Services;


        public BasicTestServer()
        {
            // this.server = new TestServer(new WebHostBuilder()
            //     .UseEnvironment("Development")
            //     .UseConfiguration(new ConfigurationBuilder()
            //         .AddJsonFile($"appsettings.Development.json", optional: false)
            //         .Build())
            //         .UseStartup<Program>());

            application = new WebApplicationFactory<Program>();
                    // .WithWebHostBuilder(builder =>{
                    //     // builder.ConfigureServices(services =>
                    //     // {
                    //     //     // services.AddDbContext(connString);
                    //     //     services.AddDAOs();
                    //     //     services.AddServices();
                    //     // });

                    // });

            // scope = services.CreateScope();

        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置受控狀態 (受控物件)
                    application!.Dispose();
                }

                // TODO: 釋出非受控資源 (非受控物件) 並覆寫完成項
                // TODO: 將大型欄位設為 Null

                application = null;
                disposedValue = true;
            }
        }

        // // TODO: 僅有當 'Dispose(bool disposing)' 具有會釋出非受控資源的程式碼時，才覆寫完成項
        // ~BasicTestServer()
        // {
        //     // 請勿變更此程式碼。請將清除程式碼放入 'Dispose(bool disposing)' 方法
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // 請勿變更此程式碼。請將清除程式碼放入 'Dispose(bool disposing)' 方法
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}