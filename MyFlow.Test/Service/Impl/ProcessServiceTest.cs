using System.Diagnostics;
using System.Dynamic;
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

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
            dynamic? applyData = new ExpandoObject();
            applyData.FlowId = 1;
            applyData.StageId = 1;
            applyData.FormId = 1;
            applyData.ProxyUser = "Test"; 

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

        [Test, Order(2)]
        public async Task testAgree()
        {
            dynamic? approveData = new ExpandoObject();
            approveData.FlowId = 1;
            approveData.StageId = 2;
            approveData.FormId = 2;
            approveData.ApplyId = 1;
            approveData.ApprId = 1;

            var userInfo = new UserInfoVM(){
                UserId = "Test",
                UserName = "Test",
            };
            try
            {
                await service!.Agree(approveData, userInfo);
            } 
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        
        [Test, Order(2)]
        public async Task testDisagree()
        {
            dynamic? approveData = new ExpandoObject();
            approveData.FlowId = 1;
            approveData.StageId = 2;
            approveData.FormId = 2;
            approveData.ApplyId = 1;
            approveData.ApprId = 1;

            var userInfo = new UserInfoVM(){
                UserId = "Test",
                UserName = "Test",
            };
            try
            {
                await service!.Disagree(approveData, userInfo);
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
