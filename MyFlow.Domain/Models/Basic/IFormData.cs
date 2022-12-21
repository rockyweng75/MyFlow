using System.Dynamic;

namespace MyFlow.Domain.Models
{
    public interface IFormData 
    {
        public int? FlowId { get; set; }
        public int? StageId { get; set; }
        public int? FormId { get; set; }
        public int? ApplyId { get; set; }
        public int? ApprId { get; set; }
    }

    public class FormData: IFormData
    {
        public int? FlowId { get; set; }
        public int? StageId { get; set; }
        public int? FormId { get; set; }
        public int? ApplyId { get; set; }
        public int? ApprId { get; set; }
    }

    
}
