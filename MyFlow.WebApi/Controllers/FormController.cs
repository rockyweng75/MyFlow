using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private IFormService service;
        private IFormItemService formItemService;
        private IActionFormService actionFormService;
        private IFlowchartService flowchartService;

        public FormController(
            IFormService service, 
            IFormItemService formItemService,
            IActionFormService actionFormService,
            IFlowchartService flowchartService
        ) 
        {
            this.service = service;
            this.formItemService = formItemService;
            this.actionFormService = actionFormService;
            this.flowchartService = flowchartService;
        }
        // GET: api/<FormController>
        [HttpGet]
        public async Task<IEnumerable<FormVM>> Get(FormVM vm)
        {
            return await service.GetList(vm);
        }

        [HttpGet("Apply/{Id}")]
        public async Task<dynamic> GetList(int Id)
        {
            var flowchart = await flowchartService.Get(Id);

            var actionForm = await actionFormService
                    .GetFirst(flowchart!);

            var formItem = await formItemService
                    .GetList(actionForm);

            return new
            {
                FlowId = flowchart!.Id,
                FlowName = flowchart!.FlowName,
                Items = formItem,
                ActionForm = actionForm
            };
        }

        // GET api/<FormController>/5
        [HttpGet("{id}")]
        public async Task<FormVM?> Get(int id)
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
