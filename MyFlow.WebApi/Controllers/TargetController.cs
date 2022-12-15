using Microsoft.AspNetCore.Mvc;
using MyFlow.Service.Targets;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private IServiceProvider serviceProvider;

        public TargetController(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var types = serviceProvider.GetServices<ITarget>();
            IList<dynamic> result = new List<dynamic>();
            foreach(var t in types)
            {
                result.Add(new { Key = t.Name, Value = t.GetType().Name });
            }
            return Ok(result);
        }
    }
}
