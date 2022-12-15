using Microsoft.AspNetCore.Mvc;
using MyFlow.Service.Deadlines;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeadlineClassController : ControllerBase
    {
        private IServiceProvider serviceProvider;

        public DeadlineClassController(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var types = serviceProvider.GetServices<IDeadline>();
            IList<dynamic> result = new List<dynamic>();
            foreach(var t in types)
            {
                result.Add(new { Key = t.Name, Value = t.GetType().Name });
            }
            return Ok(result);
        }
    }
}
