using System.Text;
using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Enums;
using MyFlow.Domain.Models.Basic;
using MyFlow.Domain.Tools;
using Dapper;
using System.Data;

namespace MyFlow.Data.DAOs
{
    public interface IApplyDataDao : IDao<ApplyData> { 
        Task<int> CountApplyHistoryList(IFilterTable filterTable, string UserId);
        Task<int> CountWaitList(IFilterTable filterTable, string UserId);
        Task<IEnumerable<WorkboardList>> GetApplyHistoryList(IFilterTable filterTable,
                                                             string UserId,
                                                             IDictionary<string, OrderMethod> order,
                                                             int PageIndex = 1,
                                                             int Rowcount = 10);
        Task<IEnumerable<dynamic>> GetDistinctApplyHistoryList(IFilterTable filterTable, string UserId);
        Task<IEnumerable<dynamic>> GetDistinctWaitList(IFilterTable filterTable, string UserId);
        Task<IEnumerable<WorkboardList>> GetWaitList(IFilterTable filterTable,
                                                     string UserId,
                                                     IDictionary<string, OrderMethod> order,
                                                     int PageIndex = 1,
                                                     int Rowcount = 10);
   
    }

    public class ApplyDataDao : BasicDao<ApplyData>, IApplyDataDao
    {
        private DbContext dbContext;

        private IDbConnection dbConnection;

        public ApplyDataDao(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.dbConnection = dbContext.Database.GetDbConnection();
        }

        private static string selectColumn =
            @" t1.*, t2.*, t3.StageName, t4.FlowName ";

        private static string fromAndJoin =
            @" 
                from ApplyData t1  
                left Join ApproveData t2
                on t1.Id = t2.ApplyId
                and t2.Id = (
                    select max(r2.Id)
                    from ApproveData r2
                    where r2.ApplyId = t1.Id
                    and (r2.StatusCode is null or t1.StatusCode <> 'S')
                    group by r2.ApplyId
                )
                left join Stage t3 
                on t3.Id = t2.StageId
                join Flowchart t4
                on t4.Id = t1.FlowId
                ";

        private static string pagedSql =
            @" OFFSET @PageSize * (@PageIndex - 1) ROWS
               FETCH NEXT @PageSize ROWS ONLY ";


        public async Task<int> CountWaitList(IFilterTable filterTable, string UserId)
        {
            var sql = @$"select count(1)
                    {fromAndJoin} 
                    Where t1.ApplyUser = @ApplyUser 
                    and t1.StatusCode = 0
                    and t2.CloseDate >= GETDATE()
                    ";

            var sqlParam = MakeFilterSql(filterTable);
            sqlParam.Params.Add("ApplyUser", UserId);

            var _sql = $"{sql}{sqlParam.Sql}";
            var result = await dbConnection.QueryFirstAsync<int>(sql, sqlParam.Params);
            return result;
        }

        public async Task<IEnumerable<WorkboardList>> GetWaitList(
            IFilterTable filterTable,
            string UserId,
            IDictionary<string, OrderMethod> order,
            int PageIndex = 1,
            int Rowcount = 10)
        {

            var sqlParam = MakeFilterSql(filterTable);

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
                var column = ColumnName(keyValue.Key);

                if (keyValue.Value == OrderMethod.Ascending)
                {
                    orderSql.Append($" {column} asc ");
                }
                else
                {
                    orderSql.Append($" {column} desc ");
                }
            }
            sqlParam.Params.Add("UserId", UserId);
            sqlParam.Params.Add("StatusCode", new int[] { (int)StatusCode.送出 });
            sqlParam.Params.Add("PageSize", Rowcount);
            sqlParam.Params.Add("PageIndex", PageIndex);

            var sql = @$" select
                    {selectColumn}
                    {fromAndJoin} 
                    Where t1.ApplyUser = @UserId 
                    and t1.StatusCode in ('S', 'C') 
                    and t2.CloseData >= GETDATE()
                    {sqlParam.Sql}
                    {orderSql.ToString()} {pagedSql}";

            var result = await dbConnection.QueryAsync<WorkboardList>(sql, sqlParam.Params);
            return result.ToList();
        }

        public async Task<IEnumerable<dynamic>> GetDistinctWaitList(
            IFilterTable filterTable,
            string UserId)
        {

            var sqlParam = MakeFilterSql(filterTable);
            var column = ColumnName(filterTable.CurrentFilter!);

            sqlParam.Params.Add("UserId", UserId);
            sqlParam.Params.Add("StatusCode", new int[] { (int)StatusCode.送出 });

            var list = await dbConnection.QueryAsync<dynamic>(
                @$" select
                     Distinct {column}
                    {fromAndJoin} 
                    Where t1.ApplyUser = @UserId 
                    and t1.StatusCode in @StatusCode 
                    {sqlParam.Sql}",
                sqlParam.Params);
            return list.AsEnumerable();
        }

        public async Task<int> CountApplyHistoryList(
            IFilterTable filterTable,
            string UserId)
        {
            var sql = @$"select count(1)
                        {fromAndJoin} 
                    Where t1.ApplyUser = @ApplyUser 
                    and (t1.StatusCode in @StatusCode or t2.CloseDate < GETDATE()) ";

            var sqlParam = MakeFilterSql(filterTable);
            sqlParam.Params.Add("ApplyUser", UserId);
            sqlParam.Params.Add("StatusCode",
                new int[]{
                    (int)StatusCode.結案,
                    (int)StatusCode.過期,
                    (int)StatusCode.放棄,
                    (int)StatusCode.不同意,
                    (int)StatusCode.同意
                });

            var result = await dbConnection.QueryFirstOrDefaultAsync<int>(
                sql: $"{sql}{sqlParam.Sql}",
                sqlParam.Params);
            return result;

        }

        public async Task<IEnumerable<WorkboardList>> GetApplyHistoryList(
                IFilterTable filterTable,
                string UserId,
                IDictionary<string, OrderMethod> order,
                int PageIndex = 1,
                int Rowcount = 10)
        {

            var sqlParam = MakeFilterSql(filterTable);

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
                var column = ColumnName(keyValue.Key);

                if (keyValue.Value == OrderMethod.Ascending)
                {
                    orderSql.Append($" {column} asc ");
                }
                else
                {
                    orderSql.Append($" {column} desc ");
                }
            }
            sqlParam.Params.Add("UserId", UserId);
            sqlParam.Params.Add("StatusCode",
                new int[]{
                    (int)StatusCode.結案,
                    (int)StatusCode.過期,
                    (int)StatusCode.放棄,
                    (int)StatusCode.不同意,
                    (int)StatusCode.同意
                });
            sqlParam.Params.Add("PageSize", Rowcount);
            sqlParam.Params.Add("PageIndex", PageIndex);

            var sql = @$"
                    select
                        {selectColumn}
                        {fromAndJoin} 
                    Where t1.ApplyUser = @UserId 
                    and (t1.StatusCode in @StatusCode or t2.CloseDate < GETDATE())
                    {sqlParam.Sql}
                    {orderSql.ToString()} {pagedSql}";
            var list = await dbConnection.QueryAsync<WorkboardList>(
                    sql,
                    sqlParam.Params);
            return list.ToList();
        }

        public async Task<IEnumerable<dynamic>> GetDistinctApplyHistoryList(
                IFilterTable filterTable,
                string UserId)
        {

            var sqlParam = MakeFilterSql(filterTable);

            sqlParam.Params.Add("UserId", UserId);
            sqlParam.Params.Add("StatusCode", StatusCode.結案);

            var column = ColumnName(filterTable.CurrentFilter!);
            var list = await dbConnection.QueryAsync<dynamic>(
                    @$"
                    select
                        distinct {column}
                        {fromAndJoin} 
                    Where t1.ApplyUser = @UserId 
                    And t1.StatusCode = @StatusCode
                    {sqlParam.Sql}",
                    sqlParam.Params);
            return list.AsEnumerable();
        }


        private SqlParam MakeFilterSql(IFilterTable filter)
        {
            var sql = new StringBuilder();
            var result = new SqlParam();
            foreach (var f in filter.Filter)
            {
                var column = f.Key;
                var paramKey = $"{column}";
                var columnName = ColumnName(column);
                sql.Append($" and {columnName} = @{paramKey} ");
                result.Params.Add(paramKey, f.Value._Selected);
            }
            result.Sql = sql.ToString();
            return result;
        }

        private string ColumnName(string column)
        {
            var columnName = ModelHepler.GetColumnName<ApplyData>(column);
            if (columnName != null)
            {
                return $"t1.{columnName}";
            }
            else
            {
                columnName = ModelHepler.GetColumnName<ApproveData>(column);
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
            return columnName!;
        }
    }
}
