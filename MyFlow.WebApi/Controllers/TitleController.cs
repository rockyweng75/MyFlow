using Microsoft.AspNetCore.Mvc;
using MyFlow.Service.Titles;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private IServiceProvider serviceProvider;

        public TitleController(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var types = serviceProvider.GetServices<ITitle>();
            IList<dynamic> result = new List<dynamic>();
            foreach(var t in types)
            {
                result.Add(new { Key = t.Name, Value = t.GetType().Name });
            }
            return Ok(result);
        }
    }
}
