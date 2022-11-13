
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IFlowchartService : IBasicCRUDService<FlowchartVM> 
    { 
    }

    public class FlowchartService : BasicCRUDService<FlowchartDao, Flowchart, FlowchartVM>, IFlowchartService
    {
        private IFlowchartDao flowchartDao;

        public override BasicDao<Flowchart> dao {
            get{
                return (BasicDao<Flowchart>)flowchartDao;
            }
        }

        public FlowchartService(IFlowchartDao flowchartDao)
        {
            this.flowchartDao = flowchartDao;
        }
    }
}
