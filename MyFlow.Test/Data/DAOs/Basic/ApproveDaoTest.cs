using MyFlow.Data.DAOs;
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Test.Data.DAOs.Basic
{
    public class ApproveDaoTest : BasicTestServer
    {
        private IApproveDataDao? dao;
        private IServiceScope? scope;

        [SetUp]
        public void init() 
        {
            scope = services.CreateScope();
            dao = scope.ServiceProvider.GetService<IApproveDataDao>();
        }

        [Test, Order(1)]
        public void testCountTodoList()
        {
            IFilterTable filterTable = new FilterTable();

            filterTable.Filter.Add("Id", new Filter(){ Column = "Id", Values = 1 });

            var task = Task.Run(async () =>
            {
                var count = await dao!.CountTodoList(filterTable, null, null, null);
                return count;
            });
            var result = task.Result;

            Assert.IsTrue(result > 0);
        }

        [Test, Order(1)]
        public void testGetTodoList()
        {
            IFilterTable filterTable = new FilterTable();

            filterTable.Filter.Add("Id", new Filter(){ Column = "Id", Values = 1 });

            var task = Task.Run(async () =>
            {
                var count = await dao!.GetTodoList(filterTable, null, null, null);
                return count;
            });
            var result = task.Result;

            Assert.IsTrue(result.Count() > 0);
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
