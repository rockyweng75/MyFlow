using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionFormController : ControllerBase
    {
        private ActionFormService service;
        public ActionFormController(ActionFormService service) 
        {
            this.service = service;
        }
        // GET: api/<ActionFormController>
        [HttpGet]
        public async Task<IEnumerable<ActionFormVM>> Get(ActionFormVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<ActionFormController>/5
        [HttpGet("{id}")]
        public async Task<ActionFormVM> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<ActionFormController>
        [HttpPost]
        public async Task<int> Post([FromBody] ActionFormVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<ActionFormController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ActionFormVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<ActionFormController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
