using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IActionJobDao : IDao<ActionJob> 
    { 
    }
    
    public class ActionJobDao : BasicDao<ActionJob>, IActionJobDao
    {
        public ActionJobDao(DbContext dbContext) : base(dbContext) 
        {
        }
    }
}
