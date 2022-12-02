using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IStageMixDao : IDao<StageMix> 
    { 
    
    } 
    public class StageMixDao : BasicDao<StageMix>, IStageMixDao
    {
        public StageMixDao(DbContext dbContext) : base(dbContext) 
        {
        }
    }
}
