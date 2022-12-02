using Microsoft.EntityFrameworkCore;
using MyFlow.Data.Connection;
using MyFlow.Data.DAOs;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.Test.Service.Impl
{
    public class TestServiceTest
    {
        private TestDao? dao;
        private TestService? service;

        [SetUp]
        public void init()
        {
            DbContext dbcontext = new TestDbContext();
            dao = new TestDao(dbcontext);
            service = new TestService(dao);
        }


        [Test, Order(1)]
        public async Task testCreate()
        {
            var task = await Task.Run(async ()=> {
                var _result = await service!.Create(new TestVM()
                {
                    Column1 = "Test",
                    Column2 = 1
                });
                await dao!.SaveChangesAsync();
                return _result;
            });
            var result = task;
           

            Assert.IsTrue(result > 0);
        }

        private TestVM? getOne { get; set; }
        [Test, Order(2)]
        public async Task testGetList()
        {
            var task = await Task.Run(async () =>
            {
                var list = await service!.GetList(new TestVM
                {
                    Column1 = "Test",
                    Column2 = 1
                });
                return list.FirstOrDefault();
            });
            getOne = task;
            Assert.True(getOne != null);
        }

        [Test, Order(3)]
        public async Task testUpdate()
        {
            var task = await Task.Run(async() =>
            {
                getOne!.Column3 = DateTime.Now;
                await service!.Update(getOne);
                return 0;
            });
            var result = task;
        }

        [Test, Order(4)]
        public async Task testGet()
        {
            var task = await Task.Run(async () =>
            {
                return await service!.Get(getOne!.Id);
            });
            var result = task;
            Assert.True(result != null);
            Assert.True(result!.Column3 != null);
        }

        [Test, Order(5)]
        public async Task testDelete()
        {
            var task = await Task.Run(async () =>
            {
                await service!.Delete(getOne!.Id);
                return 0;
            });
            var result = task;
        }

        [TearDown]
        public void Fin()
        {
            dao!.Dispose();
            dao = null;
        }
    }
}
