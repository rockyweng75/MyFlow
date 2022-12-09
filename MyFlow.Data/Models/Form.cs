namespace MyFlow.Data.Models
{
    public partial class Form : IDataModel
    {
        public int Id { get; set; }
        public int? FormType { get; set; }
        public string? FormName { get; set; }
        public int? Close { get; set; }
    }
}
