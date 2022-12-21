using MyFlow.Data.DAOs;
using MyFlow.Test.Cases;
using MyFlow.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Domain.Enums;

namespace MyFlow.Test.Data.DAOs.Basic
{
    public class ApplyDaoTest : BasicTestServer
    {
        private IApplyDataDao? dao;
        private IServiceScope? scope;

        [SetUp]
        public void init() 
        {
            scope = services.CreateScope();
            dao = scope.ServiceProvider.GetService<IApplyDataDao>();
        }
        
        [Test, Order(1)]
        public void testCreate()
        {
            var task = Task.Run(async () =>
            {
                var testCase = new TestCase1();
                var testEntity = new ApplyData(){
                    // Id = 1,
                    FlowId = testCase.flowchartVM.Id,
                    FlowName = testCase.flowchartVM.FlowName,
                    StatusCode = (int)StatusCode.送出,
                    ApplyYear = DateTime.Now.Year,
                    ApplyUser = "A0001",
                    ApplyName = "TestName"
                };
                var _result = await dao!.Create(testEntity);
                await dao!.SaveChangesAsync();
                return _result;
            });
            var result = task.Result;

            Assert.IsTrue(result.Id > 0);
        }


       [TearDown]
        public void Fin()
        {
            scope!.Dispose();
            dao!.Dispose();
            dao = null;
        }
    }
}
