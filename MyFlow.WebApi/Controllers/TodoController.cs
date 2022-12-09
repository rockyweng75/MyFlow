using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models.Basic;
using MyFlow.Service.Impl;
using MyFlow.WebApi.Security;

namespace MyFlow.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private IApproveDataService approveDataService;

        public TodoController(ILogger<TodoController> logger,
            IApproveDataService approveDataService)
        {
            _logger = logger;
            this.approveDataService = approveDataService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetTodoList([FromQuery] SortPaginationTable pagination)
        {
            var userId = User.Identity!.Name;
            var authCodes = User.Claims
                .Where(o => o.Type == CustomClaimTypes.AuthCode)
                .Select(o => o.Value)
                .FirstOrDefault();

            IList<string>? _authCode = null;
            if (authCodes != null) 
            {
                _authCode = authCodes.Split(',').ToList();
            }

            var total = await approveDataService.CountTodoList(pagination, userId!, _authCode!);

            var list = await approveDataService.GetTodoList(userId!, _authCode!, pagination);

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
            var authCodes = User.Claims
                .Where(o => o.Type == CustomClaimTypes.AuthCode)
                .Select(o => o.Value)
                .FirstOrDefault();

            var list = await approveDataService.GetDistinctTodoList(pagination, null, userId, authCodes!.Split(',').ToList());

            return Ok(list);
        }
    }
}
