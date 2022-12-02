using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private TestService service;
        public TestController(TestService service) 
        {
            this.service = service;
        }
        // GET: api/<TestController>
        [HttpGet]
        public async Task<IEnumerable<TestVM>> Get(TestVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public async Task<TestVM?> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<TestController>
        [HttpPost]
        public async Task<int> Post([FromBody] TestVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] TestVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
