
using Microsoft.Extensions.DependencyInjection;
using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;
using MyFlow.Service.Utils;
using MyFlow.Service.Actions;
using MyFlow.Service.Actions.Forward;
using MyFlow.Service.Tags;
using MyFlow.Service.Titles;
using MyFlow.Domain.Models.Basic;
using MyFlow.Service.Validations;
using MyFlow.Domain.Tools;
using MyFlow.Service.Actions.Backward;

namespace MyFlow.Service.Impl
{
    public interface IProcessService
    {
        Task<ActionResult> Agree(dynamic formData, UserInfoVM user);
        Task<ActionResult> Disagree(dynamic formData, UserInfoVM user);
        Task<ActionResult> Submit(dynamic formData, UserInfoVM user);
        Task<ActionResult> Transfer(dynamic formData, UserInfoVM user);
        Task<FlowchartVM?> FindCurrentFlowchart(IFormData formData);
        Task<dynamic?> LoadApprove(int approveId);
        Task<IList<ApproveDataVM>> LoadProcessHistory(int applyId);
    }

    public class ProcessService : IProcessService
    {
        private IServiceProvider serviceProvider;
        private IApplyDataService applyDataService;
        private IApproveDataService approveDataService;
        private IFlowchartService flowchartService;
        private IStageService stageService;

        private IStageRouteService stageRouteService;
        private IStageJobService stageJobService;
        private IActionFormService actionFormService;
        private IActionJobService actionJobService;
        private IStageValidationService stageValidationService;


        public ProcessService(
            IServiceProvider serviceProvider,
            IApplyDataService applyDataService,
            IApproveDataService approveDataService,
            IFlowchartService flowchartService,
            IStageService stageService,
            IStageRouteService stageRouteService,
            IStageJobService stageJobService,
            IActionFormService actionFormService,
            IActionJobService actionJobService,
            IStageValidationService stageValidationService
        )
        {
            this.serviceProvider = serviceProvider;
            this.applyDataService = applyDataService;
            this.approveDataService = approveDataService;
            this.flowchartService = flowchartService;
            this.stageService = stageService;
            this.stageRouteService = stageRouteService;
            this.stageJobService = stageJobService;
            this.actionFormService = actionFormService;
            this.actionJobService = actionJobService;
            this.stageValidationService = stageValidationService;
        }

        private async Task Submit(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData)
        {
            var actionForm = currentStage.ActionFormList!
               .Where(o => o.ActionType == (int)ActionType.送出)
               .FirstOrDefault();
            if (actionForm == null) throw new Exception($"找不到設定的Action:{Enum.GetName(typeof(ActionType), ActionType.送出)}");
            var progName = actionForm.ActionClass!;
            var action = (IAction)serviceProvider.GetService<IForward>(progName);
            await action.Invoke(flowchart, currentStage, applyData, null);
        }

        private async Task Agree(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM approveData)
        {
            var actionForm = currentStage.ActionFormList!
               .Where(o => o.ActionType == (int)ActionType.同意)
               .FirstOrDefault();
            if (actionForm == null) throw new Exception($"找不到設定的Action:{Enum.GetName(typeof(ActionType), ActionType.同意)}");
            var progName = actionForm.ActionClass;
            var action = (IAction)serviceProvider.GetService<IForward>(progName!);
            await action.Invoke(flowchart, currentStage, applyData, approveData);
        }

        private async Task Disagree(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM approveData)
        {
            var actionForm = currentStage.ActionFormList!
                   .Where(o => o.ActionType == (int)ActionType.不同意)
                   .FirstOrDefault();
            if (actionForm == null) throw new Exception($"找不到設定的Action:{Enum.GetName(typeof(ActionType), ActionType.不同意)}");
            var progName = actionForm.ActionClass!;
            var action = (IAction)serviceProvider.GetService<IBackward>(progName);
            await action.Invoke(flowchart, currentStage, applyData, approveData);
        }


        private async Task Transfer(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM approveData)
        {
            var actionForm = currentStage.ActionFormList!
                   .Where(o => o.ActionType == (int)ActionType.轉送)
                   .FirstOrDefault();
            if (actionForm == null) throw new Exception($"找不到設定的Action:{Enum.GetName(typeof(ActionType), ActionType.轉送)}");
            var progName = actionForm.ActionClass!;
            var action = (IAction)serviceProvider.GetService<IBackward>(progName);
            await action.Invoke(flowchart, currentStage, applyData, approveData);
        }

        private Task Fail(FlowchartVM flowchart, StageVM currentStage, ActionFormVM actionForm, ApplyDataVM applyData, ApproveDataVM? approveData = null)
        {
            // var progName = actionForm.ActionClass;
            // var action = (IAction)serviceProvider.GetClass<IErrorAction>(progName);
            // await action.Invoke(flowchart, currentStage, data);
            return Task.FromResult("");
        }


        private Task Cancel(FlowchartVM flowchart, StageVM currentStage, ApplyDataVM applyData, ApproveDataVM approveData)
        {
            // await stageDataService.UpdateTodoToCancel(data.ApplyData.ApplyId.Value);
            // var applyData = data.ApplyData;
            // applyData.StatusCode = StatusCodeConstants.撤銷.Value;
            // applyData.UpdatedDate = DateTime.Now;
            // await applyDataService.Update(applyData);
            return Task.FromResult("");
        }


        private async Task<StageVM?> FindCurrentStage(FlowchartVM flowchart, IFormData formData)
        {
            try
            {
                if(!formData.StageId.HasValue)
                {
                    throw new Exception(" StageId is null");
                }

                StageVM? stage = await stageService.GetMix(formData.StageId.Value);
                return stage;
            }
            catch (Exception e)
            {
                throw new Exception(" Get Stage Throw Exception", e);
            }
        }

        private Task<ActionFormVM> FindCurrentActionForm(StageVM stage, ActionType actionType, IFormData formData)
        {
            try
            {
                ActionFormVM actionForm = stage.ActionFormList!
                                    .Where(o => o.ActionType == (int)actionType)
                                    .First();
                return Task.FromResult(actionForm);
            }
            catch (Exception e)
            {
                throw new Exception(" Get Action form Throw Exception", e);
            }
        }

        private async Task<string?> GetTitle(FlowchartVM flowchart, ApplyDataVM applyData)
        {
            try
            {
                if (string.IsNullOrEmpty(flowchart.TitleFormat))
                    throw new Exception(" Title Format Is Empty");

                var titleClass = serviceProvider.GetService<ITitle>(flowchart.TitleFormat);

                return await titleClass.Invoke(applyData);
            }
            catch (Exception e)
            {
                throw new Exception(" Get Title Throw Exception", e);
            }
        }

        private async Task<string?> GetTag(FlowchartVM flowchart, ApplyDataVM applyData)
        {
            try
            {
                if (string.IsNullOrEmpty(flowchart.TagFormat))
                    throw new Exception(" Tag Format Is Empty");

                var tagClass = serviceProvider.GetService<ITag>(flowchart.TagFormat);

                return await tagClass.Invoke(applyData);
            }
            catch (Exception e)
            {
                throw new Exception(" Get Tag Throw Exception", e);
            }
        }

        private Task<ActionResult> ValidateTime(ActionResult actionResult, ApproveDataVM approveData)
        {

            if (approveData.CloseDate.HasValue && approveData.CloseDate.Value < DateTime.Now)
            {
                actionResult.Success = false;
                actionResult.Msg = "表單已到期";
            }

            return Task.FromResult(actionResult);
        }

        private async Task<ActionResult> ValidateForm(FlowchartVM flowchart, StageVM stage, dynamic formData, UserInfoVM user)
        {
            try
            {
                var errorList = new List<string>();

                foreach (var stageValidation in stage.StageValidationList!)
                {
                    var validation = (IValidation)serviceProvider.GetService<IValidation>(stageValidation.ValidateClass!);
                    var error = await validation.Invoke(flowchart, stage, formData, user);

                    if (!string.IsNullOrEmpty(error))
                    {
                        errorList.Add(error);
                    }
                }
                if (errorList.Count == 0)
                {
                    return new ActionResult()
                    {
                        Success = true
                    };
                }
                else
                {
                    return new ActionResult()
                    {
                        Success = false,
                        Msg = string.Join(",", errorList),
                    };
                }
            }
            catch (Exception e)
            {
                throw new Exception(" exec Validation Throw Exception", e);
            }
        }

        private Task<ActionResult> ValidateStatus(ActionResult actionResult, ApproveDataVM approveData)
        {
            if (approveData.StatusCode != null)
            {
                actionResult.Success = false;
                actionResult.Msg = $"表單已{Enum.GetName(typeof(StatusCode), approveData.StatusCode)}";
            }

            return Task.FromResult(actionResult);
        }


        public async Task<ActionResult> Submit(dynamic formData, UserInfoVM user)
        {
            FlowchartVM? flowchart;
            StageVM? currentStage = null;
            ActionFormVM? currentActionForm = null;

            if (user == null)
            {
                throw new Exception(" User is null");
            }

            try
            {

                IFormData _formData = FormDataTools.Parse(formData);

                flowchart = await FindCurrentFlowchart(_formData);
                currentStage = await FindCurrentStage(flowchart!, _formData);
                currentActionForm = await FindCurrentActionForm(currentStage!, ActionType.送出, _formData);

                try
                {
                    formData.FlowId = flowchart!.Id;
                    formData.ApplyUser = user.UserId;
                    formData.ApplyName = user.UserName;
                    formData.ApplyDept = user.DeptCode;

                    var applyData = new ApplyDataVM()
                    {
                        FlowId = flowchart.Id,
                        FlowName = flowchart.FlowName,
                        ApplyUser = user.UserId,
                        ApplyName = user.UserName,
                        DeptCode = user.DeptCode,
                        DeptName = user.DeptName,
                        StatusCode = (int)StatusCode.送出,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        ApplyYear = DateTime.Now.Year,
                        FormData = Newtonsoft.Json.JsonConvert.SerializeObject(formData)
                    };

                    var tag = await GetTag(flowchart, applyData);

                    applyData.Tag = tag;

                    var title = await GetTitle(flowchart, applyData);

                    applyData.Title = title;

                    ActionResult actionResult = await ValidateForm(flowchart, currentStage, formData, user);

                    if (actionResult.Success)
                    {
                        var applyId = await applyDataService.Create(applyData);
                        applyData.Id = applyId;
                        await Submit(flowchart, currentStage!, applyData);

                        actionResult.Msg = "提交成功";
                    }

                    return actionResult;
                }
                catch (Exception)
                {
                    // await doFail(flowchart, currentStage, null);
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult> Agree(dynamic formData, UserInfoVM user)
        {
            FlowchartVM? flowchart;
            StageVM? currentStage = null;
            ActionFormVM? currentActionForm = null;

            if (user == null)
            {
                throw new Exception(" User is null");
            }

            try
            {
                IFormData _formData = FormDataTools.Parse(formData);

                flowchart = await FindCurrentFlowchart(_formData);
                currentStage = await FindCurrentStage(flowchart!, _formData);
                currentActionForm = await FindCurrentActionForm(currentStage!, ActionType.同意, _formData);

                try
                {

                    ApplyDataVM applyData = await applyDataService.Get(formData.ApplyId);

                    ApproveDataVM source = await approveDataService.Get(formData.ApprId);

                    formData.FlowId = flowchart!.Id;
                    formData.ApplyUser = user.UserId;
                    formData.ApplyName = user.UserName;
                    formData.ApplyDept = user.DeptCode;

                    source.StatusCode = (int)StatusCode.同意;
                    source.SubmitDate = DateTime.Now;
                    source.FormData = Newtonsoft.Json.JsonConvert.SerializeObject(formData);
                    source.UserId = user.UserId;
                    source.DeptCode = user.DeptCode;
                    source.DeptName = user.DeptName;
                    source.UserName = user.UserName;

                    var actionResult = new ActionResult() { Success = true };

                    actionResult = await ValidateTime(actionResult, source);

                    actionResult = await ValidateStatus(actionResult, source);

                    actionResult = await ValidateForm(flowchart, currentStage, formData, user);

                    if (actionResult.Success)
                    {
                        await approveDataService.Update(source);

                        await Agree(flowchart, currentStage!, applyData, source);

                        actionResult.Msg = "已簽核";
                    }

                    return actionResult;
                }
                catch (Exception)
                {
                    // await doFail(flowchart, currentStage, null);
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult> Disagree(dynamic formData, UserInfoVM user)
        {

            FlowchartVM? flowchart = null;
            StageVM? currentStage = null;
            ActionFormVM? currentActionForm = null;

            if (user == null)
            {
                throw new Exception(" User is null");
            }

            try
            {
                IFormData _formData = FormDataTools.Parse(formData);

                flowchart = await FindCurrentFlowchart(_formData);
                currentStage = await FindCurrentStage(flowchart!, _formData);
                currentActionForm = await FindCurrentActionForm(currentStage!, ActionType.不同意, _formData);

                try
                {
                    ApplyDataVM applyData = await applyDataService.Get(formData.ApplyId);

                    ApproveDataVM source = await approveDataService.Get(formData.ApprId);

                    var actionResult = new ActionResult() { Success = true };

                    actionResult = await ValidateTime(actionResult, source);

                    actionResult = await ValidateStatus(actionResult, source);

                    actionResult = await ValidateForm(flowchart, currentStage, formData, user);

                    if (actionResult.Success)
                    {
                        source.StatusCode = (int)StatusCode.不同意;
                        source.SubmitDate = DateTime.Now;
                        source.FormData = Newtonsoft.Json.JsonConvert.SerializeObject(formData);
                        source.UserId = user.UserId;
                        source.UserName = user.UserName;

                        await approveDataService.Update(source);

                        await Disagree(flowchart!, currentStage!, applyData, source);

                        actionResult.Msg = "提交成功";
                    }
                    return actionResult;
                }
                catch (Exception)
                {
                    // await doFail(flowchart, currentStage, null);
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult> Transfer(dynamic formData, UserInfoVM user)
        {
            FlowchartVM? flowchart = null;
            StageVM? currentStage = null;
            ActionFormVM? currentActionForm = null;

            if (user == null)
            {
                throw new Exception(" User is null");
            }

            try
            {
                IFormData _formData = FormDataTools.Parse(formData);

                flowchart = await FindCurrentFlowchart(_formData);
                currentStage = await FindCurrentStage(flowchart!, _formData);
                currentActionForm = await FindCurrentActionForm(currentStage!, ActionType.轉送, _formData);

                try
                {
                    ApplyDataVM applyData = await applyDataService.Get(formData.ApplyId);

                    ApproveDataVM source = await approveDataService.Get(formData.ApprId);

                    var actionResult = new ActionResult() { Success = true };

                    actionResult = await ValidateTime(actionResult, source);

                    actionResult = await ValidateStatus(actionResult, source);

                    actionResult = await ValidateForm(flowchart, currentStage, formData, user);

                    if (actionResult.Success)
                    {
                        source.StatusCode = (int)StatusCode.轉單;
                        source.SubmitDate = DateTime.Now;
                        source.FormData = Newtonsoft.Json.JsonConvert.SerializeObject(formData);
                        source.UserId = user.UserId;
                        source.UserName = user.UserName;

                        await approveDataService.Update(source);

                        await Transfer(flowchart!, currentStage!, applyData, source);

                        actionResult.Msg = "提交成功";
                    }
                    return actionResult;
                }
                catch (Exception)
                {
                    // await doFail(flowchart, currentStage, null);
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FlowchartVM?> FindCurrentFlowchart(IFormData formData)
        {
            var flowchart = await flowchartService.GetMix(formData.FlowId!.Value);

            if(flowchart != null)
            {
                flowchart!.StageRouteList = await stageRouteService.GetList(flowchart!);

                flowchart!.StageValidationList = await stageValidationService.GetList(flowchart!);
                        
                flowchart!.StageJobList = await stageJobService.GetList(flowchart!);

                flowchart!.ActionFormList = await actionFormService.GetList(flowchart!);

                flowchart!.ActionJobList = await actionJobService.GetList(flowchart!);
            }

            return flowchart;
        }

        public async Task<dynamic?> LoadApprove(int approveId)
        {
            var approveData = await approveDataService.Get(approveId);

            if(approveData != null)
            {
                var applyData = await applyDataService.Get(approveData.ApplyId!.Value);
                var actionForms = await actionFormService.GetList(new ActionFormVM() { StageId = approveData.StageId });
                return new { 

                    ApplyData = applyData,
                    ApproveData = approveData,
                    ActionForms = actionForms
                };
            }
            return null;
        }

        public async Task<IList<ApproveDataVM>> LoadProcessHistory(int applyId)
        {
            var approveList = await approveDataService.GetList(new ApplyDataVM(){ Id = applyId});
            return approveList;
        }
    }
}
