using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IActionJobDao : IDao<ActionJob> 
    { 
        Task<IList<ActionJob>> GetList(Flowchart flowchart);
    }
    
    public class ActionJobDao : BasicDao<ActionJob>, IActionJobDao
    {
        private DbContext dbContext;

        public ActionJobDao(DbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<ActionJob>> GetList(Flowchart flowchart)
        {
          var queryable =
                dbContext.Set<ActionJob>().FromSqlInterpolated(
                $@"
                    select a.* 
                    from ActionJob a
                    where exists (
                        select 1 from Flowchart f 
                        join Stage s on f.Id = s.FlowId 
                        join ActionForm ff on a.ActionId = ff.Id
                        where f.Id = {flowchart.Id}
                        and s.Id = ff.StageId
                    )
                ");
            return await queryable.ToListAsync();
        } 
    }
}
