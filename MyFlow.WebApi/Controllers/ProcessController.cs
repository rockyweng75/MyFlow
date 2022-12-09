using Microsoft.AspNetCore.Mvc;
using MyFlow.Domain.Models;
using MyFlow.Domain.Tools;
using MyFlow.Service.Impl;
using MyFlow.WebApi.Security;

namespace MyFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private IProcessService processService;
        private IFlowchartService flowchartService;
        private IStageService stageService;

        public ProcessController(
            IProcessService processService, 
            IFlowchartService flowchartService,
            IStageService stageService)
        {
            this.processService = processService;
            this.flowchartService = flowchartService;
            this.stageService = stageService;
        }

        [HttpPost("/Agree")]
        public async Task<IActionResult> Agree([FromBody] dynamic formData)
        {
            var user = User.ToUserInfo();
            IFormData _formData = FormDataTools.Parse(formData);

            var flowchart = await processService.FindCurrentFlowchart(formData);
            var result = await processService.Agree(formData, user);
            return Ok(result);
        }

        [HttpPost("/Disagree")]
        public async Task<IActionResult> Disagree([FromBody] dynamic formData)
        {
            var user = User.ToUserInfo();
            IFormData _formData = FormDataTools.Parse(formData);

            var flowchart = await processService.FindCurrentFlowchart(formData);
            var result = await processService.Disagree(formData, user);
            return Ok(result);
        }

        [HttpPost("/Submit")]
        public async Task<IActionResult> Submit([FromBody] dynamic formData)
        {
            var user = User.ToUserInfo();
            IFormData _formData = FormDataTools.Parse(formData);

            var flowchart = await processService.FindCurrentFlowchart(formData);
            var result = await processService.Submit(formData, user);
            return Ok(result);
        }

        [HttpPost("/Transfer")]
        public async Task<IActionResult> Transfer([FromBody] dynamic formData)
        {
            var user = User.ToUserInfo();
            IFormData _formData = FormDataTools.Parse(formData);

            var flowchart = await processService.FindCurrentFlowchart(formData);
            var result = await processService.Transfer(formData, user);
            return Ok(result);
        }

    }
}