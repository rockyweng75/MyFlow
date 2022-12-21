﻿using MyFlow.Data.DAOs;
using MyFlow.Test.Cases;
using MyFlow.Domain.Tools;
using MyFlow.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MyFlow.Test.Data.DAOs.Basic
{
    public class ActionJobDaoTest : BasicTestServer
    {
        private IActionJobDao? dao;
        private IServiceScope? scope;

        [SetUp]
        public void init() 
        {
            scope = services.CreateScope();
            dao = scope.ServiceProvider.GetService<IActionJobDao>();
        }

        [Test, Order(1)]
        public void testCreate()
        {
            var task = Task.Run(async () =>
            {
                var testCase = new TestCase1();
                var testEntity = new ActionJob();
                ObjectClone.Clone(testCase.flowchartVM, testEntity);
                var _result = await dao!.Create(testEntity);
                await dao!.SaveChangesAsync();
                return _result;
            });
            var result = task.Result;

            Assert.IsTrue(result.Id > 0);
        }

        private MyFlow.Data.Models.ActionJob? getOne { get; set; }
        [Test, Order(2)]
        public void testGetList()
        {
            var task = Task.Run(async () =>
            {
                
                var list = await dao!.GetList(new ActionJob()
                {
                    Id = 1
                });

                var first = list.FirstOrDefault();
                return first;      
            });

            getOne = task.Result;
            Assert.True(getOne != null);
        }

        [Test, Order(3)]
        public void testUpdate()
        {
            var task = Task.Run(async () =>
            {
                getOne!.OrderId = 1;
                dao!.Update(getOne);
                await dao.SaveChangesAsync();
                return 0;
            });
            var result = task.Result;
        }

        [Test, Order(4)]
        [TestCase(1)]
        public void testGet(int Id)
        {
            var task = Task.Run(async () =>
            {
                return await dao!.Get(Id);
            });
            var result = task.Result;
            Assert.True(result != null);
        }


        [Test, Order(5)]
        [TestCase(1)]
        public void testGetList(int Id)
        {
            var task = Task.Run(async () =>
            {
                return await dao!.GetList(new Flowchart(){ Id = 1 });
            });
            var result = task.Result;
        }

        [Test, Order(999)]
        public void testDelete()
        {
            var task = Task.Run(async () =>
            {
                dao!.Delete(getOne!);
                await dao.SaveChangesAsync();
                return 0;
            });
            var result = task.Result;
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
