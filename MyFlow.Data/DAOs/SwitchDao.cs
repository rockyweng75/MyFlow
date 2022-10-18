using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IStageValidationDao : IDao<StageValidation> { 
    
    } 
    public class StageValidationDao : BasicDao<StageValidation>, IStageValidationDao
    {
        public StageValidationDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
