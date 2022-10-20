using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyDataController : ControllerBase
    {
        private ApplyDataService service;
        public ApplyDataController(ApplyDataService service) 
        {
            this.service = service;
        }
        // GET: api/<ApplyDataController>
        [HttpGet]
        public async Task<IEnumerable<ApplyDataVM>> Get(ApplyDataVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<ApplyDataController>/5
        [HttpGet("{id}")]
        public async Task<ApplyDataVM> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<ApplyDataController>
        [HttpPost]
        public async Task<int> Post([FromBody] ApplyDataVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<ApplyDataController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ApplyDataVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<ApplyDataController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
