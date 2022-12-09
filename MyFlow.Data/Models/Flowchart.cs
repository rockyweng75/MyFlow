namespace MyFlow.Data.Models
{

    public interface IFlowchart 
    {
        public int Id { get; set; }
        public string? FlowName { get; set; }
        public string? FlowEname { get; set; }
        public int? FlowType { get; set; }
        public string? AdminUser { get; set; }
        public string? Target { get; set; }
        public int? Close { get; set; }
        public string? TagFormat { get; set; }
        public string? TitleFormat { get; set; }
    }

    public partial class Flowchart : IFlowchart, IDataModel
    {
        public int Id { get; set; }
        public string? FlowName { get; set; }
        public string? FlowEname { get; set; }
        public int? FlowType { get; set; }
        public string? AdminUser { get; set; }
        public string? Target { get; set; }
        public int? Close { get; set; }
        public string? TagFormat { get; set; }
        public string? TitleFormat { get; set; }

    }
}
