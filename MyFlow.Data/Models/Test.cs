namespace MyFlow.Data.Models
{
    public partial class Test : IDataModel
    {
        public int Id { get; set; }
        public string? Column1 { get; set; }
        public int? Column2 { get; set; }
        public DateTime? Column3 { get; set; }
    }
}
