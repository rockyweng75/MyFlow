using Microsoft.Extensions.DependencyInjection;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;
using MyFlow.Test.Cases;

namespace MyFlow.Test.Service.Impl
{
    public class StageServiceTest : BasicTestServer
    {
        private IStageService? service;

        private IServiceScope? scope;

        [SetUp]
        public void init()
        {
            scope = services.CreateScope();
            service = scope.ServiceProvider.GetService<IStageService>();
        }


        [Test, Order(1)]
        public void testCreate()
        {
            var vm = new TestCase1().stageVMs[0];
            var task = Task.Run(async ()=> {
                var _result = await service!.Create(vm);
                return _result;
            });
            var result = task.Result;
           
            Assert.IsTrue(result > 0);
        }

        private StageVM? getOne { get; set; }
        [Test, Order(2)]
        public void testGetList()
        {
            var task = Task.Run(async () =>
            {
                var list = await service!.GetList(new StageVM(){Id = 1});
                return list.FirstOrDefault();
            });
            getOne = task.Result;
            Assert.True(getOne != null);
        }

        [Test, Order(3)]
        public void testUpdate()
        {
            var task = Task.Run(async() =>
            {
                getOne!.StageName = "Test";
                await service!.Update(getOne);
                return 0;
            });
            var result = task.Result;
        }

        [Test, Order(4)]
        public void testGet()
        {
            var task = Task.Run(async () =>
            {
                return await service!.Get(getOne!.Id);
            });
            var result = task.Result;
            Assert.True(result != null);
            Assert.True(result!.StageName != null);
        }
        

        [Test, Order(5)]
        public void testDelete()
        {
            var task = Task.Run(async () =>
            {
                await service!.Delete(getOne!.Id);
                return 0;
            });
            var result = task;
        }


        [Test, Order(6)]
        public void testGetFlowchartAndStage()
        {
            var task = Task.Run(async () =>
            {
                return await service!.Get(getOne!.Id);
            });
            var result = task.Result;
            Assert.True(result != null);
            Assert.True(result!.StageName != null);
        }

        [TearDown]
        public void Fin()
        {
            scope!.Dispose();
            service = null;
        }
    }
}
