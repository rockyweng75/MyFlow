
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IStageService : IBasicCRUDService<StageVM> 
    { 
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
    }
}
