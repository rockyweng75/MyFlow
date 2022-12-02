using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageValidationController : ControllerBase
    {
        private StageValidationService service;
        public StageValidationController(StageValidationService service) 
        {
            this.service = service;
        }
        // GET: api/<StageValidationController>
        [HttpGet]
        public async Task<IEnumerable<StageValidationVM>> Get(StageValidationVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<StageValidationController>/5
        [HttpGet("{id}")]
        public async Task<StageValidationVM?> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<StageValidationController>
        [HttpPost]
        public async Task<int> Post([FromBody] StageValidationVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<StageValidationController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] StageValidationVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<StageValidationController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
