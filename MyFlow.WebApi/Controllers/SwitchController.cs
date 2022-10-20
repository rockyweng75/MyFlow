using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchController : ControllerBase
    {
        private SwitchService service;
        public SwitchController(SwitchService service) 
        {
            this.service = service;
        }
        // GET: api/<SwitchController>
        [HttpGet]
        public async Task<IEnumerable<SwitchVM>> Get(SwitchVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<SwitchController>/5
        [HttpGet("{id}")]
        public async Task<SwitchVM> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<SwitchController>
        [HttpPost]
        public async Task<int> Post([FromBody] SwitchVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<SwitchController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] SwitchVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<SwitchController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
