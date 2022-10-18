using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class FlowchartService : BasicCRUDService<FlowchartDao, Flowchart, FlowchartVM>
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
