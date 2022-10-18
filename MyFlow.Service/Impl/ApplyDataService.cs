
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class ApplyDataService : BasicCRUDService<ApplyDataDao, ApplyData, ApplyDataVM>
    {
        private IApplyDataDao applyDataDao;

        public override BasicDao<ApplyData> dao {
            get{
                return (BasicDao<ApplyData>)applyDataDao;
            }
        }

        public ApplyDataService(IApplyDataDao applyDataDao)
        {
            this.applyDataDao = applyDataDao;
        }
    }
}
