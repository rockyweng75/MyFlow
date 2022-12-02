using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IFlowchartMixDao : IDao<FlowchartMix> { 
    
    } 
    public class FlowchartMixDao : BasicDao<FlowchartMix>, IFlowchartMixDao
    {
        public FlowchartMixDao(DbContext dbContext) : base(dbContext) 
        {
        }
    }
}
