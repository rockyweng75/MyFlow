namespace MyFlow.Domain.Models.Basic
{    
    public class SortPaginationTable : IPagination, ISortingTable, IFilterTable
    {
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public string? SOrder { get; set; }
        public IDictionary<string, OrderMethod> Order { get; set; }
            = new Dictionary<string, OrderMethod>();

        public string? SFilter { get; set; }

        public string? CurrentFilter { get; set; }

        public IDictionary<string, Filter> Filter { get; set; }
                = new Dictionary<string, Filter>();

    }
}

  
  
