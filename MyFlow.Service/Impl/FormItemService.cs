
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class FormItemService : BasicCRUDService<FormItemDao, FormItem, FormItemVM>
    {
        private IFormItemDao formItemDao;

        public override BasicDao<FormItem> dao {
            get{
                return (BasicDao<FormItem>)formItemDao;
            }
        }

        public FormItemService(IFormItemDao formItemDao)
        {
            this.formItemDao = formItemDao;
        }
    }
}
