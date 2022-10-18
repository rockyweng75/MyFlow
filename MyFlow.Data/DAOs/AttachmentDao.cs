using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IAttachmentDao : IDao<Attachment> { 
    
    } 
    public class AttachmentDao : BasicDao<Attachment>, IAttachmentDao
    {
        public AttachmentDao(DbContext dbContext) : base(dbContext) 
        {
        
        }
    }
}
