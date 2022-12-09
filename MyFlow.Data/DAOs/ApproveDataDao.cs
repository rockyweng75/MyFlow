using System.Text;
using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models.Basic;
using MyFlow.Domain.Tools;
using Dapper;
using System.Data;

namespace MyFlow.Data.DAOs
{
    public interface IApproveDataDao : IDao<ApproveData>
    { 
        Task<int> CountApproveList(IFilterTable filterTable, string UserId);
        Task<int> CountTodoList(IFilterTable filterTable, int? ApplyId = null, string? UserId = null, IList<string>? AuthCode = null);
        Task<IEnumerable<WorkboardList>> GetApproveList(IFilterTable filterTable,
                                                        string UserId,
                                                        IDictionary<string, OrderMethod> order,
                                                        int PageIndex = 1,
                                                        int Rowcount = 10);
        Task<IEnumerable<dynamic>> GetDistinctFromApproveList(string UserId, IFilterTable filterTable);
        Task<IEnumerable<WorkboardList>> GetTodoList(IFilterTable filterTable,
                                                     int? ApplyId = null,
                                                     string? UserId = null,
                                                     IList<string>? AuthCode = null,
                                                     IDictionary<string, OrderMethod>? order = null,
                                                     int PageIndex = 1,
                                                     int Rowcount = 10);
        Task<IEnumerable<WorkboardList>> GetTodoListByApplyId(int ApplyId);

        Task<IEnumerable<dynamic>> GetDistinctFromTodoListByColumn(
            IFilterTable filterTable,
            int? ApplyId = null,
            string? UserId = null,
            IList<string>? AuthCode = null
            );
    }

    public class ApproveDataDao : BasicDao<ApproveData>, IApproveDataDao
    {
        private DbContext dbContext;
        private IDbConnection dbConnection;

        public ApproveDataDao(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.dbConnection = dbContext.Database.GetDbConnection();
        }

        private static string querySql =
            @"select 
                {0}
            from ApproveData t1
            join ApplyData t2 
            on t1.ApplyId = t2.ID 
            join Stage t3
            on t3.Id = t1.StageId
            join Flowchart t4
            on t4.Id = t1.FlowId";

        //同步件未處理，表單執行順序在前者，不需要看後者是否已經簽核
        private static string queryColumn = @"                
                t1.*,
                t2.*, 
                t3.StageName,
                t4.FlowName
                 ";

        private static string pagedSql =
            @" OFFSET @PageSize * (@PageIndex - 1) ROWS
               FETCH NEXT @PageSize ROWS ONLY ";


        public async Task<IEnumerable<WorkboardList>> GetTodoListByApplyId(int ApplyId)
        {
            var sql = @$"{string.Format(querySql, queryColumn)} 
                        where t1.ApplyId = @ApplyId
                        and t1.StatusCode is null
                        order by t1.Id desc";
            var result = await dbConnection.QueryAsync<WorkboardList>(
                sql,
                new { ApplyId = ApplyId });
            return result.AsEnumerable();
        }

        private static string FromTodoListSql(
            int? ApplyId,
            string? UserId,
            IList<string>? AuthCode,
            string? SelectColumnSql)
        {
            var paramsSql = new StringBuilder();

            if (ApplyId.HasValue)
            {
                paramsSql.Append(" and t1.ApplyId = @ApplyId ");
            }

            if (AuthCode != null)
            {
                paramsSql.Append(" and Exists( select value from STRING_SPLIT(t1.UserId, ',') where value in @AuthCode ) ");
                if (!string.IsNullOrEmpty(UserId))
                {
                    AuthCode.Add(UserId);
                }
            }
            else if (!string.IsNullOrEmpty(UserId))
            {
                paramsSql.Append(" and t1.UserId = @UserId ");
            }
            var columnSql =
                @$"{SelectColumnSql}";

            var sql = @$"
                        {string.Format(querySql, columnSql)}
                        where t1.StatusCode is null 
                        and t1.CloseDate > CAST( GETDATE() AS Date ) 
                        {paramsSql.ToString()}";
            return sql;
        }

        public async Task<int> CountTodoList(
            IFilterTable filterTable,
            int? ApplyId = null,
            string? UserId = null,
            IList<string>? AuthCode = null)
        {

            string sql = FromTodoListSql(ApplyId, UserId, AuthCode, " t1.* ");
            var sqlParam = MakeFilterSql(filterTable);
            sql = $"{sql}{sqlParam.Sql}";

            sqlParam.Params.Add("ApplyId", ApplyId);
            sqlParam.Params.Add("UserId", UserId);
            sqlParam.Params.Add("AuthCode", AuthCode);

            var count = await dbConnection.QueryFirstAsync<int>(sql, sqlParam.Params);
            return count;
        }

        public async Task<IEnumerable<WorkboardList>> GetTodoList(
            IFilterTable filterTable,
            int? ApplyId = null,
            string? UserId = null,
            IList<string>? AuthCode = null,
            IDictionary<string, OrderMethod>? order = null,
            int PageIndex = 1,
            int Rowcount = 10)
        {

            string sql = FromTodoListSql(ApplyId, UserId, AuthCode, queryColumn);
            var sqlParam = MakeFilterSql(filterTable);
            sql = $"{sql}{sqlParam.Sql}";

            var orderSql = new StringBuilder();

            orderSql.Append(" order by ");
            if (order == null || order.Count == 0)
            {
                order = new Dictionary<string, OrderMethod>();
                order.Add("CreatedDate", OrderMethod.Descending);
            }

            var index = 0;

            foreach (var keyValue in order)
            {
                if (index++ > 0) orderSql.Append(",");

                var columnName = ColumnName(keyValue.Key);

                if (keyValue.Value == OrderMethod.Ascending)
                {
                    orderSql.Append($" {columnName} asc ");
                }
                else
                {
                    orderSql.Append($" {columnName} desc ");
                }
            }

            sqlParam.Params.Add("ApplyId", ApplyId);
            sqlParam.Params.Add("UserId", UserId);
            sqlParam.Params.Add("AuthCode", AuthCode);
            sqlParam.Params.Add("PageSize", Rowcount);
            sqlParam.Params.Add("PageNumber", PageIndex);

            var list = await dbConnection.QueryAsync<WorkboardList>(sql, sqlParam.Params);
            return list.ToList();
        }

        public async Task<IEnumerable<dynamic>> GetDistinctFromTodoListByColumn(
            IFilterTable filterTable,
            int? ApplyId = null,
            string? UserId = null,
            IList<string>? AuthCode = null
            )
        {
            var columnName = ColumnName(filterTable.CurrentFilter!);
            string sql = FromTodoListSql(ApplyId, UserId, AuthCode,
                $" distinct {columnName} ");

            var sqlParam = MakeFilterSql(filterTable);
            sql = $"{sql}{sqlParam.Sql}";

            sqlParam.Params.Add("ApplyId", ApplyId);
            sqlParam.Params.Add("UserId", UserId);
            sqlParam.Params.Add("AuthCode", AuthCode);

            return await dbConnection.QueryAsync<dynamic>(sql, sqlParam.Params);
        }


        private static string approveHistorySql =
            $@"{querySql}
                where t1.UserId = @UserId
                and t1.SubmitData is not null
            ";


        public async Task<int> CountApproveList(
            IFilterTable filterTable,
            string UserId)
        {

            var sql = $"{string.Format(approveHistorySql, " count(1) ")}";
            var sqlParam = MakeFilterSql(filterTable);
            sql = $"{sql}{sqlParam.Sql}";
            sqlParam.Params.Add("UserId", UserId);
            return await dbConnection.QueryFirstOrDefaultAsync<int>(
                sql,
                sqlParam.Params
            );
        }


        public async Task<IEnumerable<dynamic>> GetDistinctFromApproveList(
              string UserId,
              IFilterTable filterTable)
        {

            var columnName = ColumnName(filterTable.CurrentFilter!);
            var sql = @$"{string.Format(
                        approveHistorySql,
                        $" distinct {columnName} "
                    )}";

            var sqlParam = MakeFilterSql(filterTable);
            sql = $"{sql}{sqlParam.Sql}";
            sqlParam.Params.Add("UserId", UserId);

            return await dbConnection.QueryAsync<dynamic>(
                        sql: sql,
                        sqlParam.Params
                    );
        }


        public async Task<IEnumerable<WorkboardList>> GetApproveList(
            IFilterTable filterTable,
            string UserId,
            IDictionary<string, OrderMethod> order,
            int PageIndex = 1,
            int Rowcount = 10)
        {

            var sql = string.Format(approveHistorySql, queryColumn);
            var sqlParam = MakeFilterSql(filterTable);
            sql = $"{sql}{sqlParam.Sql}";

            var orderSql = new StringBuilder();
            orderSql.Append(" order by ");
            if (order.Count == 0)
            {
                order.Add("CreatedDate", OrderMethod.Descending);
            }
            var index = 0;

            foreach (var keyValue in order)
            {
                if (index++ > 0) orderSql.Append(",");
                var columnName = ColumnName(keyValue.Key);

                if (keyValue.Value == OrderMethod.Ascending)
                {
                    orderSql.Append($" {columnName} asc ");
                }
                else
                {
                    orderSql.Append($" {columnName} desc ");
                }
            }

            sqlParam.Params.Add("UserId", UserId);
            sqlParam.Params.Add("PageSize", Rowcount);
            sqlParam.Params.Add("PageNumber", PageIndex);

            var list = await dbConnection.QueryAsync<WorkboardList>(
                        sql: $"{sql}{orderSql.ToString()}{pagedSql}",
                        sqlParam.Params
                    );

            return list.ToList();
        }


        private SqlParam MakeFilterSql(IFilterTable filter)
        {
            var sql = new StringBuilder();
            var result = new SqlParam();
            foreach (var f in filter.Filter)
            {
                if (f.Value._Selected.Count > 0)
                {
                    var column = f.Key;
                    var paramKey = $"@{column}";
                    var columnName = ColumnName(column);
                    sql.Append($" and {columnName} = {f.Value._Selected} ");
                    result.Params.Add(paramKey, f.Value._Selected);
                }
            }
            result.Sql = sql.ToString();
            return result;
        }

        private string ColumnName(string column)
        {
            var columnName = ModelHepler.GetColumnName<ApproveData>(column);
            if (columnName != null)
            {
                return $"t1.{columnName}";
            }
            else
            {
                columnName = ModelHepler.GetColumnName<ApplyData>(column);
            }
            if (columnName != null)
            {
                return $"t2.{columnName}";
            }
            else
            {
                columnName = ModelHepler.GetColumnName<Stage>(column);
            }
            if (columnName != null)
            {
                return $"t3.{columnName}";
            }
            else
            {
                columnName = ModelHepler.GetColumnName<Flowchart>(column);
            }
            if (columnName != null)
            {
                return $"t4.{columnName}";
            }

            // if (columnName == null) {
            //     columnName = ModelHepler.GetCustomColumnName<E_STAGE_DATA>(column);
            // }
            // if (columnName == null)
            // {
            //     columnName = ModelHepler.GetCustomColumnName<E_APPLY_DATA>(column);
            // }
            // if (columnName == null)
            // {
            //     columnName = ModelHepler.GetCustomColumnName<E_STAGE>(column);
            // }
            // if (columnName == null)
            // {
            //     columnName = ModelHepler.GetCustomColumnName<E_FLOWCHART>(column);
            // }

            return columnName!;
        }
    }
}
