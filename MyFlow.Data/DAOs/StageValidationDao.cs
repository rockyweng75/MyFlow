using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using Switch = MyFlow.Data.Models.Switch;

namespace MyFlow.Data.DAOs
{
    public interface ISwitchDao : IDao<Switch> { 
    
    } 
    public class SwitchDao : BasicDao<Switch>, ISwitchDao
    {
        public SwitchDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
