
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class JobLogService : BasicCRUDService<JobLogDao, JobLog, JobLogVM>
    {
        private IJobLogDao jobLogDao;

        public override BasicDao<JobLog> dao {
            get{
                return (BasicDao<JobLog>)jobLogDao;
            }
        }

        public JobLogService(IJobLogDao jobLogDao)
        {
            this.jobLogDao = jobLogDao;
        }
    }
}
