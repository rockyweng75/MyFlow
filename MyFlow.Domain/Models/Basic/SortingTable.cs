namespace MyFlow.Domain.Models.Basic
{
    public interface ISortingTable
    {
        public IDictionary<string, OrderMethod> Order { get; set; }
    }
    public class SortingTable : ISortingTable
    {
        public IDictionary<string, OrderMethod> Order { get; set; }
            = new Dictionary<string, OrderMethod>();
    }

    public enum OrderMethod
    {
        Ascending,
        Descending 
    }
}
