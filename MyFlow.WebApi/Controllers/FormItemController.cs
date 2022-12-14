using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormItemController : ControllerBase
    {
        private FormItemService service;
        public FormItemController(FormItemService service) 
        {
            this.service = service;
        }
        // GET: api/<FormItemController>
        [HttpGet]
        public async Task<IEnumerable<FormItemVM>> Get(FormItemVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<FormItemController>/5
        [HttpGet("{id}")]
        public async Task<FormItemVM?> Get(int id)
        {
            return await service.Get(id);
        }


        // POST api/<FormItemController>
        [HttpPost]
        public async Task<int> Post([FromBody] FormItemVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<FormItemController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] FormItemVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<FormItemController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
