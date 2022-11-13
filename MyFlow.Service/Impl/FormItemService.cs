
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IFormItemService : IBasicCRUDService<FormItemVM> 
    { 
    }

    public class FormItemService : BasicCRUDService<FormItemDao, FormItem, FormItemVM>, IFormItemService
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
