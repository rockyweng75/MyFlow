
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IActionJobService : IBasicCRUDService<ActionJobVM> 
    { 
    }

    public class ActionJobService : BasicCRUDService<ActionJobDao, ActionJob, ActionJobVM>, IActionJobService
    {
        private IActionJobDao jobDao;

        public override BasicDao<ActionJob> dao {
            get{
                return (BasicDao<ActionJob>)jobDao;
            }
        }

        public ActionJobService(IActionJobDao jobDao)
        {
            this.jobDao = jobDao;
        }
    }
}
