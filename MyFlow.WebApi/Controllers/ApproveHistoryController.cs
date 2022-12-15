using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models.Basic;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApproveHistoryController : ControllerBase
    {
        private readonly ILogger<ApproveHistoryController> logger;
        private IApproveDataService approveDataService;

        public ApproveHistoryController(
            ILogger<ApproveHistoryController> logger,
            IApproveDataService approveDataService)
        {
            this.logger = logger;
            this.approveDataService = approveDataService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetApproveHistoryList([FromQuery] SortPaginationTable pagination)
        {
            var userId = User.Identity!.Name;

            pagination.Filter = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Filter>>(pagination.SFilter!)!;
            pagination.Order = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, OrderMethod>>(pagination.SOrder!)!;

            var total = await approveDataService.CountApproveList(pagination, userId!);

            var list = await approveDataService.GetApproveList(
                pagination,
                userId!, 
                pagination.Order, 
                pagination.PageIndex, 
                pagination.PageSize);

            return Ok(new
            {
                Data = list,
                Pager = new PaginationVM()
                {
                    PageIndex = pagination.PageIndex,
                    PageSize = pagination.PageSize,
                    TotalCount = total
                }
            });
        }

        [HttpGet("Distinct")]
        public async Task<IActionResult> getDistinctList([FromQuery] SortPaginationTable pagination)
        {
            var userId = User.Identity!.Name;

            var list = await approveDataService.GetDistinctApproveList(userId!, pagination);

            return Ok(list);
        }
    }
}
