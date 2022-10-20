using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobLogController : ControllerBase
    {
        private JobLogService service;
        public JobLogController(JobLogService service) 
        {
            this.service = service;
        }
        // GET: api/<JobLogController>
        [HttpGet]
        public async Task<IEnumerable<JobLogVM>> Get(JobLogVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<JobLogController>/5
        [HttpGet("{id}")]
        public async Task<JobLogVM> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<JobLogController>
        [HttpPost]
        public async Task<int> Post([FromBody] JobLogVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<JobLogController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] JobLogVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<JobLogController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
