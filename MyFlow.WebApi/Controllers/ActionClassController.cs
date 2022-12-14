using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Enums;
using MyFlow.Service.Actions;
using MyFlow.Service.Actions.Backward;
using MyFlow.Service.Actions.Forward;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionClassController : ControllerBase
    {
        private IServiceProvider serviceProvider;
        public ActionClassController(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
        }
        // GET: api/<ActionClassController>
        [HttpGet("{actionType}")]
        public IActionResult Get(int actionType)
        {
            Type? type = null; 
            switch(actionType)
            {
                case (int)ActionType.送出:
                case (int)ActionType.同意:
                    type = typeof(IForward);
                    break;
                case (int)ActionType.不同意:
                    type = typeof(IBackward);
                    break;
                default:
                    break;
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            if(type is not null)
            {
                var actionTypes = serviceProvider.GetServices(type);
                foreach(var a in actionTypes)
                {
                    IAction action = (IAction)a!;
                    result.Add(action.Name, action.GetType().Name);
                }
            }
            return Ok(result);
        }
    }
}
