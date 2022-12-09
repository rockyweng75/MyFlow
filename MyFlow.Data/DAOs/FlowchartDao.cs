using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IFlowchartDao : IDao<Flowchart> 
    { 
        Task<Flowchart?> GetMix(int Id);
    } 
    public class FlowchartDao : BasicDao<Flowchart>, IFlowchartDao
    {
        private DbContext dbContext;
        public FlowchartDao(DbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Flowchart?> GetMix(int Id)
        {
            var queryable = 
                await dbContext.Set<Flowchart>()
                .Where(o => o.Id == Id)
                .Select(o => 
                    new { 
                        Flowchart = o,
                        StageList = dbContext.Set<Stage>().Where(s=> s.FlowId == o.Id).ToList() 
                    })
                .SingleAsync();
                
            queryable.Flowchart.StageList = queryable.StageList;
            return queryable.Flowchart;
        }
    }
}
