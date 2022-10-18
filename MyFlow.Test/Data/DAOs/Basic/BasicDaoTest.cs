using Microsoft.EntityFrameworkCore;
using MyFlow.Data.Connection;
using MyFlow.Data.DAOs;

namespace MyFlow.Test.Data.DAOs.Basic
{
    public class BasicDaoTest
    {
        private TestDao dao;

        [SetUp]
        public void init() 
        {
            DbContext dbcontext = new TestDbContext();
            dao = new TestDao(dbcontext);
        }


        [Test, Order(1)]
        public void testCreate()
        {
            var task = Task.Run(async () =>
            {
                var _result = await dao.Create(new MyFlow.Data.Models.Test()
                {
                    Column1 = "Test",
                    Column2 = 1
                });
                await dao.SaveChangesAsync();
                return _result;
            });
            var result = task.Result;

            Assert.IsTrue(result.Id > 0);
        }

        private MyFlow.Data.Models.Test getOne { get; set; }
        [Test, Order(2)]
        public async Task testGetList()
        {
            var task = Task.Run(async () =>
            {
                var list = await dao.GetList(new MyFlow.Data.Models.Test()
                {
                    Column1 = "Test",
                    Column2 = 1
                });

                var first = list.FirstOrDefault();

                first = await dao.GetQueryable(o => o.Column1 == "Test")
                    .FirstOrDefaultAsync();
                return first;      
            });

            getOne = task.Result;
            Assert.True(getOne != null);
        }

        [Test, Order(3)]
        public async Task testUpdate()
        {
            var task = Task.Run(async () =>
            {
                getOne.Column3 = DateTime.Now;
                dao.Update(getOne);
                await dao.SaveChangesAsync();
                return 0;
            });
            var result = task.Result;
        }

        [Test, Order(4)]
        public async Task testGet()
        {
            var task = Task.Run(async () =>
            {
                return await dao.Get(getOne.Id);
            });
            var result = task.Result;
            Assert.True(result != null);
            Assert.True(result.Column3 != null);
        }

        [Test, Order(5)]
        public async Task testDelete()
        {
            var task = Task.Run(async () =>
            {
                dao.Delete(getOne);
                await dao.SaveChangesAsync();
                return 0;
            });
            var result = task.Result;
        }

        [TearDown]
        public void Fin() {
            dao.Dispose();
            dao = null;
        }
    }
}
