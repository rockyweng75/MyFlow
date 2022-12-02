
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Service.Impl
{
    public interface IFlowchartMixService : IBasicCRUDService<FlowchartVM> 
    { 
    }

    public class FlowchartMixService : BasicCRUDService<FlowchartMixDao, FlowchartMix, FlowchartVM>, IFlowchartMixService
    {
        private IFlowchartMixDao flowchartMixDao;

        public override BasicDao<FlowchartMix> dao {
            get{
                return (BasicDao<FlowchartMix>)flowchartMixDao;
            }
        }

        public FlowchartMixService(IFlowchartMixDao flowchartMixDao)
        {
            this.flowchartMixDao = flowchartMixDao;
        }
        
        public override FlowchartVM ToViewModel(IDataModel dataModel)
        {

            FlowchartMix _dataModel = (FlowchartMix)dataModel;
            FlowchartVM vm = dataModel.ToViewModel<FlowchartVM>();

            vm.StageList = _dataModel.StageList!
                    .Select(o => o.ToViewModel<StageVM>())
                    .ToList();

            return vm;
        }

        public override FlowchartMix ToDataModel(IViewModel viewModel)
        {
            return viewModel.ToDataModel<FlowchartMix>();
        }
    }
}
