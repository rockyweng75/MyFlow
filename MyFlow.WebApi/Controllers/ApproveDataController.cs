using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveDataController : ControllerBase
    {
        private ApproveDataService service;
        public ApproveDataController(ApproveDataService service) 
        {
            this.service = service;
        }
        // GET: api/<ApproveDataController>
        [HttpGet]
        public async Task<IEnumerable<ApproveDataVM>> Get(ApproveDataVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<ApproveDataController>/5
        [HttpGet("{id}")]
        public async Task<ApproveDataVM?> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<ApproveDataController>
        [HttpPost]
        public async Task<int> Post([FromBody] ApproveDataVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<ApproveDataController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ApproveDataVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<ApproveDataController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
