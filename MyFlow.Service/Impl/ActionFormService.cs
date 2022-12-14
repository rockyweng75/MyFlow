using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public interface IActionFormService : IBasicCRUDService<ActionFormVM> 
    { 
        Task<ActionFormVM?> GetMix(int Id);

        Task<IList<ActionFormVM>> GetList(FlowchartVM flowchart);

        Task<ActionFormVM> GetFirst(FlowchartVM flowchart);
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

        public async Task<ActionFormVM?> GetMix(int Id)
        {
            var actionForm = await actionFormDao.GetMix(Id);
            var result = actionForm!.ToViewModel<ActionFormVM>();
            result.ActionJobList = actionForm!.ActionJobList!.Select(o => o.ToViewModel<ActionJobVM>()).ToList();
            return result;
        } 

        public async Task<IList<ActionFormVM>> GetList(FlowchartVM flowchart)
        {
            var result = await actionFormDao.GetList(flowchart.ToDataModel<Flowchart>());
            return result!.Select(o => o.ToViewModel<ActionFormVM>()).ToList();
        }

        public async Task<ActionFormVM> GetFirst(FlowchartVM flowchart)
        {
            var result = await actionFormDao.GetFirst(flowchart.ToDataModel<Flowchart>());
            return result.ToViewModel<ActionFormVM>();
        }
    }
}
