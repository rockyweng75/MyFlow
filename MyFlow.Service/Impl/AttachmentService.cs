
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface IAttachmentService : IBasicCRUDService<AttachmentVM> 
    { 
    }

    public class AttachmentService : BasicCRUDService<AttachmentDao, Attachment, AttachmentVM>, IAttachmentService
    {
        private IAttachmentDao attachmentDao;

        public override BasicDao<Attachment> dao {
            get{
                return (BasicDao<Attachment>)attachmentDao;
            }
        }

        public AttachmentService(IAttachmentDao attachmentDao)
        {
            this.attachmentDao = attachmentDao;
        }
    }
}
