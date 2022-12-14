
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Service.Impl
{
    public interface IApproveDataService : IBasicCRUDService<ApproveDataVM> 
    { 
        Task<int> CountApproveList(IFilterTable filterTable, string UserId);
        Task<int> CountTodoList(IFilterTable filterTable, string userId, IList<string> authCodes);
        Task<IList<WorkboardList>> GetApproveList(IFilterTable filterTable, string UserId, IDictionary<string, OrderMethod> order, int PageIndex = 1, int Rowcount = 10);
        Task<IList<dynamic>> GetDistinctApproveList(string UserId, IFilterTable filterTable);
        Task<IList<dynamic>> GetDistinctTodoList(IFilterTable filterTable, int? ApplyId = null, string? UserId = null, IList<string>? AuthCodes = null);
        Task<IList<WorkboardList>> GetTodoList(string userId, IList<string> authCodes, SortPaginationTable pagination);
        Task<IList<ApproveDataVM>> GetList(ApplyDataVM applyDataVM);
    }

    public class ApproveDataService : BasicCRUDService<ApproveDataDao, ApproveData, ApproveDataVM>, IApproveDataService
    {
        private IApproveDataDao approveDataDao;

        public override BasicDao<ApproveData> dao
        {
            get
            {
                return (BasicDao<ApproveData>)approveDataDao;
            }
        }

        public ApproveDataService(IApproveDataDao approveDataDao)
        {
            this.approveDataDao = approveDataDao;
        }

        public async Task<IList<ApproveDataVM>> GetList(ApplyDataVM applyDataVM)
        {
            return await this.GetList(new ApproveDataVM(){ApplyId = applyDataVM.Id});
        }


        public async Task<int> CountTodoList(
            IFilterTable filterTable,
            string userId,
            IList<string> authCodes)
        {
            return await approveDataDao.CountTodoList(filterTable, null, userId, authCodes);
        }

        public async Task<IList<WorkboardList>> GetTodoList(
            string userId,
            IList<string> authCodes,
            SortPaginationTable pagination)
        {
            pagination.Order = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, OrderMethod>>(pagination.SOrder!)!;

            var list = await approveDataDao.GetTodoList(
                pagination,
                null,
                userId,
                authCodes,
                pagination.Order,
                pagination.PageIndex,
                pagination.PageSize);

            return list.ToList();
        }


        public async Task<IList<dynamic>> GetDistinctTodoList(
            IFilterTable filterTable,
            int? ApplyId = null,
            string? UserId = null,
            IList<string>? AuthCodes = null
            )
        {
            var task = await approveDataDao.GetDistinctFromTodoListByColumn(filterTable, ApplyId, UserId, AuthCodes);
            var result = task.Select(o =>
            {
                var map = o as IDictionary<string, object>;
                return map!.Values.FirstOrDefault();
            }).ToList();
            return result!;
        }


        public async Task<int> CountApproveList(IFilterTable filterTable, string UserId)
        {
            return await approveDataDao.CountApproveList(filterTable, UserId);
        }

        public async Task<IList<dynamic>> GetDistinctApproveList(
            string UserId,
            IFilterTable filterTable)
        {
            var task = await approveDataDao.GetDistinctFromApproveList(UserId, filterTable);
            var result = task.Select(o =>
            {
                var map = o as IDictionary<string, object>;
                return map!.Values.FirstOrDefault();
            }).ToList();
            return result!;
        }


        public async Task<IList<WorkboardList>> GetApproveList(
            IFilterTable filterTable,
            string UserId,
            IDictionary<string, OrderMethod> order,
            int PageIndex = 1,
            int Rowcount = 10)
        {
            try
            {
                var task = await approveDataDao.GetApproveList(filterTable, UserId, order, PageIndex, Rowcount);
                return task.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
