using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IFormItemDao : IDao<FormItem> { 
    
    } 
    public class FormItemDao : BasicDao<FormItem>, IFormItemDao
    {
        public FormItemDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
