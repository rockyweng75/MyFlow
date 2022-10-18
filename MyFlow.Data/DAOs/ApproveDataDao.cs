using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IApproveDataDao : IDao<ApproveData> { 
    
    } 
    public class ApproveDataDao : BasicDao<ApproveData>, IApproveDataDao
    {
        public ApproveDataDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
