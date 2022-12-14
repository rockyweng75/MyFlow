
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public interface IFormService : IBasicCRUDService<FormVM> 
    { 
    }

    public class FormService : BasicCRUDService<FormDao, Form, FormVM>, IFormService
    {
        private IFormDao formDao;
        private IFormItemDao formItemDao;
        private IActionFormDao actionFormDao;

        public override BasicDao<Form> dao {
            get{
                return (BasicDao<Form>)formDao;
            }
        }

        public FormService(
            IFormDao formDao, 
            IFormItemDao formItemDao,
            IActionFormDao actionFormDao
        )
        {
            this.formDao = formDao;
            this.formItemDao = formItemDao;
            this.actionFormDao = actionFormDao;
        }
        

    }
}
