
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class DeadlineService : BasicCRUDService<DeadlineDao, Deadline, DeadlineVM>
    {
        private IDeadlineDao deadlineDao;

        public override BasicDao<Deadline> dao {
            get{
                return (BasicDao<Deadline>)deadlineDao;
            }
        }

        public DeadlineService(IDeadlineDao deadlineDao)
        {
            this.deadlineDao = deadlineDao;
        }
    }
}
