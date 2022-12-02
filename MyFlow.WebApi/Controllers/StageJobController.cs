using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageJobController : ControllerBase
    {
        private StageJobService service;
        public StageJobController(StageJobService service) 
        {
            this.service = service;
        }
        
        // GET: api/<JobController>
        [HttpGet]
        public async Task<IEnumerable<StageJobVM>> Get(StageJobVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<JobController>/5
        [HttpGet("{id}")]
        public async Task<StageJobVM?> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<JobController>
        [HttpPost]
        public async Task<int> Post([FromBody] StageJobVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<JobController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] StageJobVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<JobController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
