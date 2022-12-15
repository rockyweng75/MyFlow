using Microsoft.AspNetCore.Mvc;
using MyFlow.Service.Tags;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private IServiceProvider serviceProvider;

        public TagController(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var types = serviceProvider.GetServices<ITag>();
            IList<dynamic> result = new List<dynamic>();
            foreach(var t in types)
            {
                result.Add(new { Key = t.Name, Value = t.GetType().Name });
            }
            return Ok(result);
        }
    }
}
