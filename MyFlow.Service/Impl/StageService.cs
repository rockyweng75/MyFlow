
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class StageService : BasicCRUDService<StageDao, Stage, StageVM>
    {
        private IStageDao stageDao;

        public override BasicDao<Stage> dao
        {
            get
            {
                return (BasicDao<Stage>)stageDao;
            }
        }

        public StageService(IStageDao stageDaoDao)
        {
            this.stageDao = stageDaoDao;
        }
    }
}

