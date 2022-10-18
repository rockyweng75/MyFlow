using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IFlowchartDao : IDao<Flowchart> { 
    
    } 
    public class FlowchartDao : BasicDao<Flowchart>, IFlowchartDao
    {
        public FlowchartDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
