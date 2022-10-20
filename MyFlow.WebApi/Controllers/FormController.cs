using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private FormService service;
        public FormController(FormService service) 
        {
            this.service = service;
        }
        // GET: api/<FormController>
        [HttpGet]
        public async Task<IEnumerable<FormVM>> Get(FormVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<FormController>/5
        [HttpGet("{id}")]
        public async Task<FormVM> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<FormController>
        [HttpPost]
        public async Task<int> Post([FromBody] FormVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<FormController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] FormVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<FormController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
