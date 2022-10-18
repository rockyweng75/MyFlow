
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class ApproveDataService : BasicCRUDService<ApproveDataDao, ApproveData, ApproveDataVM>
    {
        private IApproveDataDao approveDataDao;

        public override BasicDao<ApproveData> dao {
            get{
                return (BasicDao<ApproveData>)approveDataDao;
            }
        }

        public ApproveDataService(IApproveDataDao approveDataDao)
        {
            this.approveDataDao = approveDataDao;
        }
    }
}
