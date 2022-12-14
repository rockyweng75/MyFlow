using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IActionFormDao : IDao<ActionForm> { 
        Task<ActionForm?> GetMix(int Id);
        Task<IList<ActionForm>> GetList(Flowchart flowchart);
        Task<ActionForm> GetFirst(Flowchart flowchart);
    } 
    public class ActionFormDao : BasicDao<ActionForm>, IActionFormDao
    {
        private DbContext dbContext;
        public ActionFormDao(DbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<ActionForm?> GetMix(int Id)
        {
            var queryable = 
                await dbContext.Set<ActionForm>()
                .Where(o => o.Id == Id)
                .Select(o => 
                    new { 
                        ActionForm = o,
                        ActionJobList = dbContext.Set<ActionJob>().Where(s=> s.ActionId == o.Id).ToList() 
                    })
                .SingleAsync();
                
            queryable.ActionForm.ActionJobList = queryable.ActionJobList;
            return queryable.ActionForm;
        } 


        public async Task<ActionForm> GetFirst(Flowchart flowchart)
        {
          var queryable =
                dbContext.Set<ActionForm>().FromSqlInterpolated(
                $@"
                    select a.* 
                    from ActionForm a
                    where a.StageId =  
                        (
                            select top(1) s.Id 
                            from Flowchart f 
                            join Stage s on f.Id = s.FlowId 
                            where f.Id = { flowchart.Id }
                            and a.StageId = s.Id
                        )
                    order by a.OrderId
                ");
            return await queryable.FirstAsync();
        } 


        public async Task<IList<ActionForm>> GetList(Flowchart flowchart)
        {
          var queryable =
                dbContext.Set<ActionForm>().FromSqlInterpolated(
                $@"
                    select a.* 
                    from ActionForm a
                    where exists (
                        select 1 from Flowchart f 
                        join Stage s on f.Id = s.FlowId 
                        where f.Id = {flowchart.Id}
                        and a.StageId = s.Id
                    )
                ");
            return await queryable.ToListAsync();
        } 
    }
}
