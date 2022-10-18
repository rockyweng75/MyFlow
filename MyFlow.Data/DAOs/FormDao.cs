using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IFormDao : IDao<Form> { 
    
    } 
    public class FormDao : BasicDao<Form>, IFormDao
    {
        public FormDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
