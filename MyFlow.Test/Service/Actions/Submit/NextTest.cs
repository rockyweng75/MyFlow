using Microsoft.Extensions.DependencyInjection;
using MyFlow.Service.Actions.Submit;
using MyFlow.Service.Actions;
using MyFlow.Test.Cases;
using MyFlow.Domain.Models;
using System.Diagnostics;

namespace MyFlow.Test.Service.Actions.Submit
{
    public class NextTest : BasicTestServer
    {
        private IServiceScope? scope;

        private IAction? action;

        [SetUp]
        public void init() 
        {
            scope = services.CreateScope();
            action = scope.ServiceProvider.GetServices<ISubmit>()
                        .Where(o => o.GetType().Name == "Next")
                        .FirstOrDefault();
        }

        [Test, Order(1)]
        public async Task testInvoke()
        {
            var testCase = new TestCase1();

            var flowchart = testCase.flowchartVM;
            var stage = testCase.stageVMs
                .Where( o => o.FlowId == flowchart.Id)
                .Where( o => o.OrderId == 1)
                .FirstOrDefault();

            flowchart.StageList = testCase.stageVMs;
            flowchart!.ActionFormList = testCase.actionFormVMs
                .Where(o => o.StageId == stage!.Id)
                .ToList();

            flowchart.StageRouteList = testCase.routeVMs;

            var applyData = new ApplyDataVM(){
                FlowId = flowchart.Id,
                FlowName = flowchart.FlowName,
                ApplyUser = "Test",
                ApplyName = "Test",
                FormData = Newtonsoft.Json.JsonConvert.SerializeObject(new { ProxyUser = "Test" })
            };
            var approveData = new ApproveDataVM();
            try
            {
                await action!.Invoke(flowchart, stage!, applyData, null);
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
        }
    }
}
