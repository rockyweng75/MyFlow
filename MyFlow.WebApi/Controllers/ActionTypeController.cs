using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

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
            var result = new Dictionary<string, int>();
            for (var i = 0; i < keys.Length; i++)
            {
                result.Add(keys[i], (int)values.GetValue(i)!);
            }
            return Ok(result);
        }
    }
}
