
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IFormItemService : IBasicCRUDService<FormItemVM> 
    { 
        Task<IList<FormItemVM>> GetList(FlowchartVM flowchart);
        Task<IList<FormItemVM>> GetList(ActionFormVM actionForm);
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

        public async Task<IList<FormItemVM>> GetList(FlowchartVM flowchart)
        {
            var formItems = await formItemDao.GetList(new Flowchart(){ Id = flowchart.Id });

            return formItems
                    .Select(o => o.ToViewModel<FormItemVM>())
                    .ToList();
        }

        public async Task<IList<FormItemVM>> GetList(ActionFormVM actionForm)
        {
            var formItems = await formItemDao.GetList(new ActionForm(){ Id = actionForm.Id});

            return formItems
                    .Select(o => o.ToViewModel<FormItemVM>())
                    .ToList();
        }
    }
}
