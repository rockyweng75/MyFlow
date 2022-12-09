using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IStageValidationDao : IDao<StageValidation> 
    { 
        Task<IList<StageValidation>> GetList(Flowchart flowchart);
    } 
    public class StageValidationDao : BasicDao<StageValidation>, IStageValidationDao
    {
        private DbContext dbContext;
        public StageValidationDao(DbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<StageValidation>> GetList(Flowchart flowchart)
        {
            var queryable = dbContext.Set<StageValidation>()
                                .Where(o => o.FlowId == flowchart.Id);

            return await queryable.ToListAsync();
        }

    }
}
