
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public interface IStageMixService : IBasicCRUDService<StageVM> 
    { 
    }

    public class StageMixService : BasicCRUDService<StageMixDao, StageMix, StageVM>, IStageMixService
    {
        private IStageMixDao stageDao;

        public override BasicDao<StageMix> dao {
            get{
                return (BasicDao<StageMix>)stageDao;
            }
        }

        public StageMixService(IStageMixDao stageDao)
        {
            this.stageDao = stageDao;
        }
    }
}
