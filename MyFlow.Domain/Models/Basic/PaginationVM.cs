namespace MyFlow.Domain.Models.Basic
{
    [Serializable]
    public class PaginationVM : IViewModel
    {
        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }
    }
}
