
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public interface IStageValidationService : IBasicCRUDService<StageValidationVM> 
    { 
        Task<IList<StageValidationVM>> GetList(FlowchartVM flowchart);
    }

    public class StageValidationService : BasicCRUDService<StageValidationDao, StageValidation, StageValidationVM>, IStageValidationService
    {
        private IStageValidationDao stageValidationDao;

        public override BasicDao<StageValidation> dao {
            get{
                return (BasicDao<StageValidation>)stageValidationDao;
            }
        }

        public StageValidationService(IStageValidationDao stageValidationDao)
        {
            this.stageValidationDao = stageValidationDao;
        }

        public async Task<IList<StageValidationVM>> GetList(FlowchartVM flowchart)
        {
            var result = await stageValidationDao.GetList(flowchart.ToDataModel<Flowchart>());
            return result!.Select(o => o.ToViewModel<StageValidationVM>()).ToList();
        }

    }
}
