using System;
using System.Collections.Generic;

namespace MyFlow.Domain.Models
{
    public partial class AttachmentVM : IViewModel
    {
        public int Id { get; set; }
        public int? ApplyId { get; set; }
        public int? ApprId { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? ContentType { get; set; }
        public byte[]? FileData { get; set; }
    }
}
