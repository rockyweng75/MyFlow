using Dapper;

namespace MyFlow.Data.Models
{
    public class SqlParam
    {
        public string? Sql { get; set; }

        public DynamicParameters Params { get; set; } = new DynamicParameters();
    }
}
