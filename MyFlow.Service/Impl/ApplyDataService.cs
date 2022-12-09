
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Service.Impl
{
    public interface IApplyDataService : IBasicCRUDService<ApplyDataVM> 
    { 
        Task<int> CountApplyHistoryList(IFilterTable filterTable, string UserId);
        Task<int> CountWaitList(IFilterTable filterTable, string UserId);
        Task<IList<WorkboardList>> GetApplyHistoryList(IFilterTable filterTable,
                                                       string UserId,
                                                       IDictionary<string, OrderMethod> order,
                                                       int PageIndex = 1,
                                                       int Rowcount = 10);
        Task<IList<dynamic>> GetDistinctApplyHistoryList(IFilterTable filterTable, string UserId);
        Task<IList<dynamic>> GetDistinctWaitList(IFilterTable filterTable, string UserId);
        Task<IList<WorkboardList>> GetWaitList(IFilterTable filterTable,
                                               string UserId,
                                               IDictionary<string, OrderMethod> order,
                                               int PageIndex = 1,
                                               int Rowcount = 10);
    }

    public class ApplyDataService : BasicCRUDService<ApplyDataDao, ApplyData, ApplyDataVM>, IApplyDataService
    {
        private IApplyDataDao applyDataDao;

        public override BasicDao<ApplyData> dao
        {
            get
            {
                return (BasicDao<ApplyData>)applyDataDao;
            }
        }

        public ApplyDataService(IApplyDataDao applyDataDao)
        {
            this.applyDataDao = applyDataDao;
        }

        public async Task<int> CountWaitList(
            IFilterTable filterTable,
            string UserId)
        {
            return await applyDataDao.CountWaitList(filterTable, UserId);
        }

        public async Task<IList<WorkboardList>> GetWaitList(
            IFilterTable filterTable,
            string UserId,
            IDictionary<string, OrderMethod> order,
            int PageIndex = 1,
            int Rowcount = 10)
        {
            var task = await applyDataDao.GetWaitList(filterTable, UserId, order, PageIndex, Rowcount);
            return task.ToList();
        }

        public async Task<IList<dynamic>> GetDistinctWaitList(
            IFilterTable filterTable,
            string UserId)
        {
            var task = await applyDataDao.GetDistinctWaitList(filterTable, UserId);
            var result = task.Select(o =>
            {
                var map = o as IDictionary<string, object>;
                return map!.Values.FirstOrDefault();
            }).ToList();
            return result!;
        }

        public async Task<int> CountApplyHistoryList(
            IFilterTable filterTable,
            string UserId)
        {
            return await applyDataDao.CountApplyHistoryList(filterTable, UserId);
        }

        public async Task<IList<WorkboardList>> GetApplyHistoryList(
            IFilterTable filterTable,
            string UserId,
            IDictionary<string, OrderMethod> order,
            int PageIndex = 1,
            int Rowcount = 10)
        {
            var task = await applyDataDao.GetApplyHistoryList(filterTable, UserId, order, PageIndex, Rowcount);
            return task.ToList();
        }

        public async Task<IList<dynamic>> GetDistinctApplyHistoryList(
             IFilterTable filterTable,
             string UserId)
        {
            var task = await applyDataDao.GetDistinctApplyHistoryList(filterTable, UserId);
            var result = task.Select(o =>
            {
                var map = o as IDictionary<string, object>;
                return map!.Values.FirstOrDefault();
            }).ToList();
            return result!;
        }

    }
}
