using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IStageRouteDao : IDao<StageRoute>
    { 
    } 
    public class StageRouteDao : BasicDao<StageRoute>, IStageRouteDao
    {
        public StageRouteDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
