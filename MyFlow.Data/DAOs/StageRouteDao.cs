using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IStageRouteDao : IDao<StageRoute>
    { 
        Task<IList<StageRoute>> GetList(Flowchart flowchart);
    } 
    public class StageRouteDao : BasicDao<StageRoute>, IStageRouteDao
    {
        private DbContext dbContext;
        public StageRouteDao(DbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<StageRoute>> GetList(Flowchart flowchart)
        {
            var queryable = dbContext.Set<StageRoute>()
                                .Where(o => o.FlowId == flowchart.Id);

            return await queryable.ToListAsync();
        }

    }
}
