using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Enums;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowTypeController : ControllerBase
    {
        public FlowTypeController() 
        {
        }
        // GET: api/<FormItemController>
        [HttpGet]
        public IActionResult Get()
        {
           var keys = Enum.GetNames(typeof(FlowType));
            var values = Enum.GetValues(typeof(FlowType));
            var result = new List<dynamic>();
            for (var i = 0; i < keys.Length; i++)
            {
                result.Add(new { Key = keys[i], Value = (int)values.GetValue(i)!});
            }
            return Ok(result);
        }
    }
}
