
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class ActionFormService : BasicCRUDService<ActionFormDao, ActionForm, ActionFormVM>
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
