namespace MyFlow.Data.Models
{
    public partial class FormItem : IDataModel
    {
        public int Id { get; set; }
        public int? FormId { get; set; }
        public int? OrderId { get; set; }
        public int? ItemType { get; set; }
        public string? ItemTitle { get; set; }
        public string? ItemValue { get; set; }
        public string? ItemDesc { get; set; }
        public string? ItemFormat { get; set; }
        public string? DataRef { get; set; }
        public string? Multiple { get; set; }
        public string? Disabled { get; set; }
        public string? Required { get; set; }
    }
}
