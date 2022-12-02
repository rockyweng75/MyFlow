
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IFlowchartService : IBasicCRUDService<FlowchartVM> 
    { 
        Task<FlowchartVM?> GetFlowchartAndStage(int Id);
    }

    public class FlowchartService : BasicCRUDService<FlowchartDao, Flowchart, FlowchartVM>, IFlowchartService
    {
        private IFlowchartDao flowchartDao;
        private IStageService stageService;
        private IStageRouteService stageRouteService;

        public override BasicDao<Flowchart> dao {
            get{
                return (BasicDao<Flowchart>)flowchartDao;
            }
        }

        public FlowchartService(
            IFlowchartDao flowchartDao, 
            IStageService stageService,
            IStageRouteService stageRouteService
            )
        {
            this.flowchartDao = flowchartDao;
            this.stageService = stageService;
            this.stageRouteService = stageRouteService;
        }

        public async Task<FlowchartVM?> GetFlowchartAndStage(int Id)
        {
            var flowchart = await this.Get(Id);

            if(flowchart != null)
            {
                var stageList = await stageService.GetList(new StageVM(){ FlowId = flowchart.Id });
                flowchart.StageList = stageList;

                var stageRouteList = await stageRouteService.GetList(new StageRouteVM(){ FlowId = flowchart.Id });
                flowchart.StageRouteList = stageRouteList;
            }
            
            return flowchart;
        } 


    }
}
