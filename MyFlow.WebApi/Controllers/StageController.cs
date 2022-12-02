using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private StageService service;
        public StageController(StageService service) 
        {
            this.service = service;
        }
        // GET: api/<StageController>
        [HttpGet]
        public async Task<IEnumerable<StageVM>> Get(StageVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<StageController>/5
        [HttpGet("{id}")]
        public async Task<StageVM?> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<StageController>
        [HttpPost]
        public async Task<int> Post([FromBody] StageVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<StageController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] StageVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<StageController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
