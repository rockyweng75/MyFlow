using Microsoft.AspNetCore.Mvc;
using MyFlow.Service.Switchs;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchController : ControllerBase
    {
        private IServiceProvider serviceProvider;

        public SwitchController(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var types = serviceProvider.GetServices<ISwitch>();
            IList<dynamic> result = new List<dynamic>();
            foreach(var t in types)
            {
                result.Add(new { Key = t.Name, Value = t.GetType().Name });
            }
            return Ok(result);
        }
    }
}
