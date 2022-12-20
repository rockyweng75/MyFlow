using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;
using MyFlow.Test.Cases;

namespace MyFlow.Test.Service.Impl
{
    public class ProcessServiceTest : BasicTestServer
    {
        private IProcessService? service;

        private IServiceScope? scope;

        [SetUp]
        public void init()
        {
            scope = services.CreateScope();
            service = scope.ServiceProvider.GetService<IProcessService>();
        }


        [Test, Order(1)]
        public async Task testSubmit()
        {
            var applyData = new { 
                FlowId = 1,
                StageId = 1,
                FormId = 1,
                ProxyUser = "Test" 
            };
            var userInfo = new UserInfoVM(){
                UserId = "Test",
                UserName = "Test",
            };
            try
            {
                await service!.Submit(applyData, userInfo);
            } 
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }


        [TearDown]
        public void Fin()
        {
            scope!.Dispose();
            service = null;
        }
    }
}
