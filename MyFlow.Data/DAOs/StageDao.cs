using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IStageDao : IDao<Stage> 
    { 
        Task<Stage?> GetMix(int Id);

        Task<Stage?> GetFirst(Flowchart flowchart);

        Task<IList<Stage>?> GetList(Flowchart flowchart);
    } 

    public class StageDao : BasicDao<Stage>, IStageDao
    {
        private DbContext dbContext;
        public StageDao(DbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Stage?> GetMix(int Id)
        {
            var StageList = new List<Stage>();
            var queryable = 
                await dbContext.Set<Stage>()
                .Where(o => o.Id == Id)
                .Select(o => 
                    new { 
                        Stage = o,
                        StageJob = dbContext.Set<StageJob>().Where(s=> s.StageId == o.Id).ToList(),
                        StageRoute = dbContext.Set<StageRoute>().Where(s=> s.StageId == o.Id).ToList(),
                        ActionForm = dbContext.Set<ActionForm>().Where(s=> s.StageId == o.Id).ToList(),
                        StageValidation = dbContext.Set<StageValidation>().Where(s=> s.StageId == o.Id).ToList()
                    })
                .SingleAsync();
                
            queryable.Stage.StageJobList = queryable.StageJob;
            queryable.Stage.StageRouteList = queryable.StageRoute;
            queryable.Stage.ActionFormList = queryable.ActionForm;
            queryable.Stage.StageValidationList = queryable.StageValidation;

            return queryable.Stage;
        } 

        public async Task<Stage?> GetFirst(Flowchart flowchart)
        {
            var queryable = dbContext.Set<Stage>()
                                .Where(o => o.FlowId == flowchart.Id)
                                .OrderBy(o => o.OrderId);

            return await queryable.FirstOrDefaultAsync();
        }        

        public async Task<IList<Stage>?> GetList(Flowchart flowchart)
        {
            var queryable = dbContext.Set<Stage>()
                                .Where(o => o.FlowId == flowchart.Id)
                                .OrderBy(o => o.OrderId);

            return await queryable.ToListAsync();
        }   
    }
}
