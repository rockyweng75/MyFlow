using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IApplyDataDao : IDao<ApplyData> { 
    
    } 
    public class ApplyDataDao : BasicDao<ApplyData>, IApplyDataDao
    {
        public ApplyDataDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
