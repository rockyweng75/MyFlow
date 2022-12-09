using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models.Basic;
using MyFlow.Service.Impl;
using MyFlow.WebApi.Security;

namespace MyFlow.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplyHistoryController : ControllerBase
    {
        private readonly ILogger<ApplyHistoryController> logger;
        private IApplyDataService applyDataService;

        public ApplyHistoryController(
            ILogger<ApplyHistoryController> logger,
            IApplyDataService applyDataService)
        {
            this.logger = logger;
            this.applyDataService = applyDataService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetList([FromQuery] SortPaginationTable pagination)
        {
            var userId = User.Identity!.Name;

            var total = await applyDataService.CountApplyHistoryList(pagination, userId!);

            var list = await applyDataService.GetApplyHistoryList(pagination, userId!, pagination.Order, pagination.PageIndex, pagination.PageSize);

            return Ok(new
            {
                Data = list,
                Pager = new PaginationVM()
                {
                    PageSize = pagination.PageSize,
                    PageIndex = pagination.PageIndex,
                    TotalCount = total
                }
            });
        }

        [HttpGet("Distinct")]
        public async Task<IActionResult> getDistinctList([FromQuery] SortPaginationTable pagination)
        {
            var userId = User.Identity!.Name;

            var list = await applyDataService.GetDistinctApplyHistoryList(pagination, userId!);

            return Ok(list);
        }
    }
}
