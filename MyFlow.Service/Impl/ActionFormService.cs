
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IActionFormService : IBasicCRUDService<ActionFormVM> 
    { 
    }

    public class ActionFormService : BasicCRUDService<ActionFormDao, ActionForm, ActionFormVM>, IActionFormService
    {
        private IActionFormDao actionFormDao;

        public override BasicDao<ActionForm> dao {
            get{
                return (BasicDao<ActionForm>)actionFormDao;
            }
        }

        public ActionFormService(IActionFormDao actionFormDao)
        {
            this.actionFormDao = actionFormDao;
        }
    }
}
