using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageRouteController : ControllerBase
    {
        private StageRouteService service;
        public StageRouteController(StageRouteService service) 
        {
            this.service = service;
        }
        // GET: api/<StageRouteController>
        [HttpGet]
        public async Task<IEnumerable<StageRouteVM>> Get(StageRouteVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<StageRouteController>/5
        [HttpGet("{id}")]
        public async Task<StageRouteVM?> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<StageRouteController>
        [HttpPost]
        public async Task<int> Post([FromBody] StageRouteVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<StageRouteController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] StageRouteVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<StageRouteController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
