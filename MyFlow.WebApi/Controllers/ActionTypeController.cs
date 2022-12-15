using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Enums;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionTypeController : ControllerBase
    {
        public ActionTypeController() 
        {
        }
        // GET: api/<ActionTypeController>
        [HttpGet]
        public IActionResult Get()
        {
            var keys = Enum.GetNames(typeof(ActionType));
            var values = Enum.GetValues(typeof(ActionType));
            var result = new List<dynamic>();
            for (var i = 0; i < keys.Length; i++)
            {
                result.Add(new { Key = keys[i], Value = (int)values.GetValue(i)!});
            }
            return Ok(result);
        }
    }
}
