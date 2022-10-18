
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class AttachmentService : BasicCRUDService<AttachmentDao, Attachment, AttachmentVM>
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
