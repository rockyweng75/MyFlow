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
        [HttpGet()]
        public IActionResult Get()
        {
            Type? type = null; 
            IList<dynamic> result = new List<dynamic>();
            var key = 0;
            foreach(var actionType in Enum.GetValues(typeof(ActionType)))
            {
                switch(actionType)
                {
                    case (int)ActionType.送出:
                        // eq. 同意
                        break;
                    case (int)ActionType.同意:
                        type = typeof(IForward);
                        key = (int)ActionType.同意;
                        break;
                    case (int)ActionType.不同意:
                        type = typeof(IBackward);
                        key = (int)ActionType.不同意;
                        break;
                    default:
                        break;
                }

                if(type is not null)
                {
                    var actionTypes = serviceProvider.GetServices(type);
                    Dictionary<string, string> actionVM = new Dictionary<string, string>();
                    foreach(var a in actionTypes)
                    {
                        IAction action = (IAction)a!;
                        actionVM.Add(action.Name, action.Key);
                    }

                    result.Add(new { ActionType = key, Options = actionVM });
                }
            }
     
            return Ok(result);
        }
    }
}
