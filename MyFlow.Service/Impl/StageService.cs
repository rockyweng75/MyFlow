
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IStageService : IBasicCRUDService<StageVM> 
    { 
        Task<StageVM?> GetMix(int Id);

        Task<StageVM?> GetFirst(FlowchartVM flowchart);
    }

    public class StageService : BasicCRUDService<StageDao, Stage, StageVM>, IStageService
    {
        private IStageDao stageDao;

        public override BasicDao<Stage> dao {
            get{
                return (BasicDao<Stage>)stageDao;
            }
        }

        public StageService(IStageDao stageDao)
        {
            this.stageDao = stageDao;
        }

        public async Task<StageVM?> GetMix(int Id)
        {
            var stage = await stageDao.GetMix(Id);
            
            return stage!.ToViewModel<StageVM>();
        } 

        public async Task<StageVM?> GetFirst(FlowchartVM flowchart)
        {
            var result = await stageDao.GetFirst(flowchart.ToDataModel<Flowchart>());
            return result!.ToViewModel<StageVM>();
        } 

        public async Task<IList<StageVM>> GetList(FlowchartVM flowchart)
        {
            var result = await stageDao.GetList(flowchart.ToDataModel<Flowchart>());
            return result!.Select(o => o.ToViewModel<StageVM>()).ToList();
        }

    }
}
