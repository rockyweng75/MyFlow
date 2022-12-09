using Microsoft.Extensions.DependencyInjection;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;
using MyFlow.Test.Cases;

namespace MyFlow.Test.Service.Impl
{
    public class FlowchartServiceTest : BasicTestServer
    {
        private IFlowchartService? service;

        private IServiceScope? scope;

        [SetUp]
        public void init()
        {
            // DbContext dbcontext = new TestDbContext();

            scope = services.CreateScope();
            service = scope.ServiceProvider.GetService<IFlowchartService>();
        }


        [Test, Order(1)]
        public void testCreate()
        {
            var vm = new TestCase1().flowchartVM;
            var task = Task.Run(async ()=> {
                var _result = await service!.Create(vm);
                return _result;
            });
            var result = task.Result;
           
            Assert.IsTrue(result > 0);
        }

        private FlowchartVM? getOne { get; set; }
        [Test, Order(2)]
        public void testGetList()
        {
            var task = Task.Run(async () =>
            {
                var list = await service!.GetList(new FlowchartVM(){Id = 1});
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
                getOne!.FlowName = "Test";
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
            Assert.True(result!.FlowName != null);
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

        [TearDown]
        public void Fin()
        {
            scope!.Dispose();
            service = null;
        }
    }
}
