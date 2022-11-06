using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain;
using MyFlow.Domain.Models;
using MyFlow.Domain.Models.Basic;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowchartController : ControllerBase
    {
        private FlowchartService service;
        public FlowchartController(FlowchartService service) 
        {
            this.service = service;
        }
        // GET: api/<FlowchartController>
        [HttpGet]
        public async Task<IEnumerable<FlowchartVM>> Get([FromQuery]FlowchartVM vm)
        {
            return await service.GetList(vm);
        }

        // GET api/<FlowchartController>/5
        [HttpGet("{id}")]
        public async Task<FlowchartVM> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<FlowchartController>
        [HttpPost]
        public async Task<int> Post([FromBody] FlowchartVM vm)
        {
            return await service.Create(vm);
        }

        // PUT api/<FlowchartController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] FlowchartVM vm)
        {
            await service.Update(vm);
        }

        // DELETE api/<FlowchartController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.Delete(id);
        }
    }
}
