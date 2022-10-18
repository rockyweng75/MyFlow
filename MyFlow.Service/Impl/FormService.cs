
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class FormService : BasicCRUDService<FormDao, Form, FormVM>
    {
        private IFormDao formDao;

        public override BasicDao<Form> dao {
            get{
                return (BasicDao<Form>)formDao;
            }
        }

        public FormService(IFormDao formDao)
        {
            this.formDao = formDao;
        }
    }
}
