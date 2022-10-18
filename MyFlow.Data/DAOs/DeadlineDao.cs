using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IDeadlineDao : IDao<Deadline> { 
    
    } 
    public class DeadlineDao : BasicDao<Deadline>, IDeadlineDao
    {
        public DeadlineDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
