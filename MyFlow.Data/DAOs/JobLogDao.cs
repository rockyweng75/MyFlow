using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IJobLogDao : IDao<JobLog> { 
    
    } 
    public class JobLogDao : BasicDao<JobLog>, IJobLogDao
    {
        public JobLogDao(DbContext dbContext) : base(dbContext) 
        {
        }
    }
}
