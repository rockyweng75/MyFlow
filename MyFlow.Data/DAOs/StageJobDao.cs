using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IStageJobDao : IDao<StageJob> { 
        Task<IList<StageJob>> GetList(Flowchart flowchart);
    } 
    public class StageJobDao : BasicDao<StageJob>, IStageJobDao
    {
        private DbContext dbContext;
        public StageJobDao(DbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<StageJob>> GetList(Flowchart flowchart)
        {
            var queryable =
                 dbContext.Set<StageJob>().FromSqlInterpolated(
                $@"
                    select a.* 
                    from StageJob a
                    where exists (
                        select 1 from Flowchart f 
                        join Stage s on f.Id = s.FlowId 
                        where f.Id = {flowchart.Id}
                        and a.StageId = s.Id
                    )
                ");
            
            // var queryable = dbContext.Set<StageJob>()
            //                 .Join(
            //                     dbContext.Set<Flowchart>()
            //                         .Where(o => o.Id == flowchart.Id)
            //                         .Join(
            //                             dbContext.Set<Stage>(), 
            //                             f => f.Id,
            //                             s => s.FlowId,
            //                             (f,s) => s.Id
            //                         ).AsEnumerable(),
            //                 s=> s.StageId,
            //                 f=> f,
            //                 (j, s)=> j);

            return await queryable.ToListAsync();
        }
    }
}
