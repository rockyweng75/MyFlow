using MyFlow.Data.DAOs;
using MyFlow.Test.Cases;
using MyFlow.Domain.Tools;
using MyFlow.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Service;

namespace MyFlow.Test.Data.DAOs.Basic
{
    public class InitDb : BasicTestServer
    {
        private IServiceScope? scope;
        private IFlowchartDao? flowchartDao;
        private IStageDao? stageDao;
        private IStageRouteDao? stageRouteDao;
        private IActionFormDao? actionFormDao;
        private IActionJobDao? actionJobDao;
        private IFormDao? formDao;
        private IFormItemDao? formItemDao;
        

        [SetUp]
        public void init() 
        {
            scope = services.CreateScope();
            flowchartDao = scope.ServiceProvider.GetService<IFlowchartDao>()!;
            stageDao = scope.ServiceProvider.GetService<IStageDao>()!;
            stageRouteDao = scope.ServiceProvider.GetService<IStageRouteDao>()!;
            actionFormDao = scope.ServiceProvider.GetService<IActionFormDao>()!;
            actionJobDao = scope.ServiceProvider.GetService<IActionJobDao>()!;
            formDao = scope.ServiceProvider.GetService<IFormDao>()!;
            formItemDao = scope.ServiceProvider.GetService<IFormItemDao>()!;
        }

        [Test, Order(1)]
        public void testCreate()
        {

            var testCase = new TestCase1();
            var task = Task.Run(async () =>
            {
                Flowchart flowchart = testCase.flowchartVM.ToDataModel<Flowchart>();
                var _entity = await flowchartDao!.Get(flowchart.Id);
                flowchartDao!.Delete(_entity!);
                await flowchartDao.SaveChangesAsync();
                await flowchartDao!.Create(flowchart);
                await flowchartDao.SaveChangesAsync();

                foreach(var _stage in testCase.stageVMs!)
                {
                    var stage = _stage.ToDataModel<Stage>();
                    var entity = await stageDao!.Get(stage.Id);
                    if(entity != null) {
                        stageDao!.Delete(entity!);
                        await stageDao!.SaveChangesAsync();
                    }
                    await stageDao!.Create(stage);
                    await stageDao!.SaveChangesAsync();

                }

                foreach(var _form in testCase.forms!)
                {
                    var form = _form.ToDataModel<Form>();
                    var entity = await formDao!.Get(form.Id);
                    if(entity != null) {
                        formDao!.Delete(entity!);
                        await formDao!.SaveChangesAsync();
                    }

                    await formDao!.Create(form);
                    await formDao!.SaveChangesAsync();
                }

                foreach(var _actionForm in testCase.actionFormVMs!)
                {
                    var actionForm = _actionForm.ToDataModel<ActionForm>();
                    var entity = await actionFormDao!.Get(actionForm.Id);
                    if(entity != null) {
                        actionFormDao!.Delete(entity!);
                        await actionFormDao!.SaveChangesAsync();
                    }

                    await actionFormDao!.Create(actionForm);
                    await actionFormDao!.SaveChangesAsync();
                }

                foreach(var _formItem in testCase.formItemVMs)
                {
                    var formItem = _formItem.ToDataModel<FormItem>();
                    var entity = await formItemDao!.Get(formItem.Id);
                    if(entity != null) {
                        formItemDao!.Delete(entity!);
                        await formItemDao!.SaveChangesAsync();
                    }

                    await formItemDao!.Create(formItem);
                    await formItemDao!.SaveChangesAsync();
                }

                foreach(var _route in testCase.routeVMs)
                {
                    var stageRoute = _route.ToDataModel<StageRoute>();
                    var entity = await stageRouteDao!.Get(stageRoute.Id);
                    if(entity != null) {
                        stageRouteDao!.Delete(entity!);
                        await stageRouteDao!.SaveChangesAsync();
                    }
                    await stageRouteDao!.Create(stageRoute);
                    await stageRouteDao!.SaveChangesAsync();
                }
                return 1;
            });
            var result = task.Result;

            // Assert.IsTrue(result > 0);
        }


       [TearDown]
        public void Fin()
        {
            scope!.Dispose();
        }
    }
}
