
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public interface IStageJobService : IBasicCRUDService<StageJobVM> 
    { 
    }

    public class StageJobService : BasicCRUDService<StageJobDao, StageJob, StageJobVM>, IStageJobService
    {
        private IStageJobDao jobDao;

        public override BasicDao<StageJob> dao {
            get{
                return (BasicDao<StageJob>)jobDao;
            }
        }

        public StageJobService(IStageJobDao jobDao)
        {
            this.jobDao = jobDao;
        }
    }
}
