using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowchartController : ControllerBase
    {
        private IFlowchartService service;
        private ILogger<FlowchartController> logger;
        public FlowchartController(
            ILogger<FlowchartController> logger, 
            IFlowchartService service) 
        {
            this.service = service;
            this.logger = logger;
        }
        // GET: api/<FlowchartController>
        [HttpGet]
        public async Task<IList<FlowchartItem>> Get([FromQuery]FlowchartVM vm)
        {
            var roles = User.Claims
                            .Where(o => o.Type == ClaimTypes.Role)
                            .Select(o => o.Value)
                            .ToList();

            var list = await service.GetOpenFlowchart(roles);

            var groupList = list.GroupBy(o => o.FlowType);

            var result = new List<FlowchartItem>();
            foreach (var group in groupList)
            {
                var r = new FlowchartItem();
                r.Id = (int)group.Key!;
                r.Name = Enum.GetName(typeof(FlowchartType), group.Key!);
                r.Children = new List<FlowchartItem>();
                foreach (var item in group)
                {
                    var cr = new FlowchartItem();
                    cr.Id = item.Id;
                    cr.Name = item.FlowName;
                    r.Children.Add(cr);
                }
                result.Add(r);
            }
            return result;
        }

        [HttpGet("Admin")]
        // [Authorize(Roles = "A")]
        public async Task<IList<FlowchartItem>> GetFullList([FromQuery]FlowchartVM vm)
        {
            var list = await service.GetAll();
            var groupList = list.GroupBy(o => o.FlowType);

            var result = new List<FlowchartItem>();
            foreach (var group in groupList)
            {
                var r = new FlowchartItem();
                r.Id = (int)group.Key!;
                r.Name = Enum.GetName(typeof(FlowchartType), group.Key!);
                r.Children = new List<FlowchartItem>();
                foreach (var item in group)
                {
                    var cr = new FlowchartItem();
                    cr.Id = item.Id;
                    cr.Name = item.FlowName;
                    r.Children.Add(cr);
                }
                result.Add(r);
            }
            return result;
        }


        // GET api/<FlowchartController>/5
        [HttpGet("{id}")]
        public async Task<FlowchartVM?> Get(int id)
        {
            return await service.Get(id);
        }

        // POST api/<FlowchartController>
        [HttpPost]
        // [Authorize(Roles = "A")]
        public async Task<int> Post([FromBody] FlowchartVM vm)
        {
            try
            {
                return await service.Create(vm);
            } 
            catch(Exception e)
            {
                logger.LogError($"Flowchart:{vm.Id}", e);
                throw new Exception("500");
            }
        }

        // PUT api/<FlowchartController>/5
        [HttpPut("{id}")]
        // [Authorize(Roles = "A")]
        public async Task Put(int id, [FromBody] FlowchartVM vm)
        {
            try
            {
                await service.Update(vm);
            } 
            catch(Exception e)
            {
                logger.LogError($"Flowchart:{vm.Id}", e);
                throw new Exception("500");
            }
        }

        // DELETE api/<FlowchartController>/5
        // [HttpDelete("{id}")]
        // [Authorize(Roles = "A")]
        // public async Task Delete(int id)
        // {
        //     await service.Delete(id);
        // }
    }
}
