
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public interface IStageRouteService : IBasicCRUDService<StageRouteVM> 
    { 
        Task<IList<StageRouteVM>> GetList(FlowchartVM flowchart);
    }

    public class StageRouteService : BasicCRUDService<StageRouteDao, StageRoute, StageRouteVM>, IStageRouteService
    {
        private IStageRouteDao stageRouteDao;

        public override BasicDao<StageRoute> dao {
            get{
                return (BasicDao<StageRoute>)stageRouteDao;
            }
        }

        public StageRouteService(IStageRouteDao stageRouteDao)
        {
            this.stageRouteDao = stageRouteDao;
        }

        public async Task<IList<StageRouteVM>> GetList(FlowchartVM flowchart)
        {
            var result = await stageRouteDao.GetList(flowchart.ToDataModel<Flowchart>());
            return result!.Select(o => o.ToViewModel<StageRouteVM>()).ToList();
        }
    }
}
