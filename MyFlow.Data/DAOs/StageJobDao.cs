using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IStageJobDao : IDao<StageJob> { 
    
    } 
    public class StageJobDao : BasicDao<StageJob>, IStageJobDao
    {
        public StageJobDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
