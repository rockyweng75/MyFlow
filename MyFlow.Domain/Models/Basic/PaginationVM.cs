namespace MyFlow.Domain.Models.Basic
{
    [Serializable]
    public class PaginationVM : IPagination, IViewModel
    {
        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }
    }

    public interface IPagination
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }

}
