using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IJobDao : IDao<Job> { 
    
    } 
    public class JobDao : BasicDao<Job>, IJobDao
    {
        public JobDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
