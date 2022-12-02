using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private AttachmentService service;
        public AttachmentController(AttachmentService service) 
        {
            this.service = service;
        }
        // GET: api/<AttachmentController>
        [HttpGet]
        public async Task<IEnumerable<AttachmentVM>> Get(AttachmentVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<AttachmentController>/5
        [HttpGet("{id}")]
        public async Task<AttachmentVM?> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<AttachmentController>
        [HttpPost]
        public async Task<int> Post([FromBody] AttachmentVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<AttachmentController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] AttachmentVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<AttachmentController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
