using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeadlineController : ControllerBase
    {
        private DeadlineService service;
        public DeadlineController(DeadlineService service) 
        {
            this.service = service;
        }
        
        // GET: api/<DeadlineController>
        [HttpGet]
        public async Task<IEnumerable<DeadlineVM>> Get(DeadlineVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<DeadlineController>/5
        [HttpGet("{id}")]
        public async Task<DeadlineVM?> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<DeadlineController>
        [HttpPost]
        public async Task<int> Post([FromBody] DeadlineVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<DeadlineController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] DeadlineVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<DeadlineController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
