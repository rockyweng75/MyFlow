using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IStageDao : IDao<Stage> 
    { 
    } 

    public class StageDao : BasicDao<Stage>, IStageDao
    {
        public StageDao(DbContext dbContext) : base(dbContext) 
        {
        
        }



        
    }
}
