using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IActionFormDao : IDao<ActionForm> { 
    
    } 
    public class ActionFormDao : BasicDao<ActionForm>, IActionFormDao
    {
        public ActionFormDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
