using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;

namespace MyFlow.Test.Cases
{
    public class TestCase1 
    {
        public FlowchartVM flowchartVM = new FlowchartVM()
        {
            Id = 1,
            FlowName = "請假單",
            FlowType = (int?)FlowType.人事系統,
            AdminUser = "PA",
            Target = "Personnel",
            Close = 0,
            TagFormat = "A000",
            TitleFormat = "A000"
        };

        public IList<StageVM> stageVMs = new StageVM[] { 
            new StageVM(){ 
                Id = 1,
                FlowId = 1,
                OrderId = 1,
                StageName = "申請人填單",
                Deadline = "AnyTime",
                Target = "AnyOne",
                TargetParams = ""
            },
            new StageVM(){ 
                Id = 2,
                FlowId = 1,
                OrderId = 2,
                StageName = "代理人核可",
                Deadline = "AnyTime",
                Target = "CustomTarget",
                TargetParams = "ProxyUser"
            },
            new StageVM(){
                Id = 3,
                FlowId = 1,
                OrderId = 3,
                StageName = "主管核可",
                Deadline = "AnyTime",
                Target = "PersonnelManager",
                TargetParams = ""
            }
        };

        public IList<FormVM> forms = new FormVM[] {
            new FormVM(){
                Id = 1,
                FormType = (int)FormType.使用者輸入,
                FormName = "請假單輸入",
                Close = 0,
            },
            new FormVM(){
                Id = 2,
                FormType = (int)FormType.簽核輸入,
                FormName = "通用簽核單",
                Close = 0,
            },
            new FormVM(){
                Id = 3,
                FormType = (int)FormType.簽核輸入,
                FormName = "照會單",
                Close = 0,
            },
        };


        public IList<ActionFormVM> actionFormVMs = new ActionFormVM[] {
            new ActionFormVM(){
                Id = 1,
                StageId = 1,
                OrderId = 1,
                FormId = 1,
                ActionType = (int)ActionType.送出,
                ActionName = "Submit",
                ButtonName = "送出",
                ActionClass = "Next",
            },
            new ActionFormVM(){
                Id = 2,
                StageId = 2,
                OrderId = 1,
                FormId = 2,
                ActionType = (int)ActionType.同意,
                ActionName = "Confirm",
                ButtonName = "同意",
                ActionClass = "Next",
            },
            new ActionFormVM(){
                Id = 3,
                StageId = 2,
                OrderId = 2,
                FormId = 2,
                ActionType = (int)ActionType.不同意,
                ActionName = "Rollback",
                ButtonName = "不同意",
                ActionClass = "Previous",
            },
            new ActionFormVM(){
                Id = 4,
                StageId = 3,
                OrderId = 1,
                FormId = 2,
                ActionType = (int)ActionType.同意,
                ActionName = "End",
                ButtonName = "結案",
                ActionClass = "Close",
            },
            new ActionFormVM(){
                Id = 5,
                StageId = 3,
                OrderId = 2,
                FormId = 2,
                ActionType = (int)ActionType.不同意,
                ActionName = "Withdraw",
                ButtonName = "不同意",
                ActionClass = "Withdraw",
            },
        };

       public IList<ActionJobVM> actionJobVMs = new List<ActionJobVM>()
        {
            new ActionJobVM(){ 
                Id = 1,
                ActionId = 1,
                OrderId = 1,
                JobType = (int)JobType.After,
                JobClass = "MailToApplyUser"
            },
            new ActionJobVM(){
                Id = 2,
                ActionId = 1,
                OrderId = 2,
                JobType = (int)JobType.After,
                JobClass = "MailToNextApprover"
            },
            new ActionJobVM(){
                Id = 3,
                ActionId = 2,
                OrderId = 1,
                JobType = (int)JobType.After,
                JobClass = "MailToNextApprover"
            },
            new ActionJobVM(){
                Id = 4,
                ActionId = 3,
                OrderId = 1,
                JobType = (int)JobType.After,
                JobClass = "MailForFinish",   
            },
           new ActionJobVM(){ 
                Id = 5,
                ActionId = 3,
                OrderId = 1,
                JobType = (int)JobType.After,
                JobClass = "MailToNextApprover"
            },
        };

        public IList<DeadlineVM> deadlineVM = new DeadlineVM[] {
            new DeadlineVM(){
                Id = 1,
                BeginDate = null,
                EndDate = null,
                DeadlineClass = "AnyTime",
                DeadlineRemark = "全開放"
            },
        };

        public IList<StageRouteVM> routeVMs = new StageRouteVM[] {
            new StageRouteVM(){
                Id = 1,
                FlowId = 1,
                StageId = 1,
                NextStageId = 2,
                ActionType = (int)ActionType.送出,
                SwitchClass = null,      
            },
            new StageRouteVM(){
                Id = 2,
                FlowId = 1,
                StageId = 2,
                NextStageId = 3,
                ActionType = (int)ActionType.同意,
                SwitchClass = null,
            },
            new StageRouteVM(){
                Id = 3,
                FlowId = 1,
                StageId = 2,
                NextStageId = 4,
                ActionType = (int)ActionType.同意,
                SwitchClass = null,
            },
            new StageRouteVM(){
                Id = 4,
                FlowId = 1,
                StageId = 2,
                NextStageId = 1,
                ActionType = (int)ActionType.不同意,
                SwitchClass = null,
            },
            new StageRouteVM(){
                Id = 5,
                FlowId = 1,
                StageId = 3,
                NextStageId = 0,
                ActionType = (int)ActionType.同意,
                SwitchClass = null,
            },
            new StageRouteVM(){
                Id = 6,
                FlowId = 1,
                StageId = 3,
                NextStageId = 1,
                ActionType = (int)ActionType.不同意,
                SwitchClass = null,
            },

        };

        public IList<FormItemVM> formItemVMs = new FormItemVM[] {
            new FormItemVM(){
                Id = 1,
                FormId = 1,
                OrderId = 1,
                ItemType = (int)ItemType.Input,
                ItemValue = "EmpNo",
                ItemTitle = "員工姓名",
                ItemDesc = null,
                ItemFormat = null,
                DataRef = null,
                Multiple = null,
                Disabled = "Y",
                Required = "Y"
            },
            new FormItemVM(){
                Id = 2,
                FormId = 1,
                OrderId = 2,
                ItemType = (int)ItemType.Input,
                ItemValue = "DeptNo",
                ItemTitle = "所屬部門",
                ItemDesc = null,
                ItemFormat = null,
                DataRef = null,
                Multiple = null,
                Disabled = "Y",
                Required = "Y"
            },
            new FormItemVM(){
                Id = 3,
                FormId = 1,
                OrderId = 3,
                ItemType = (int)ItemType.Select,
                ItemTitle = "假別",
                ItemValue = "LeaveOption",
                ItemDesc = null,
                ItemFormat = null,
                DataRef = "LeaveOption",
                Multiple = null,
                Disabled = null,
                Required = "Y"
            },
            new FormItemVM(){
                Id = 4,
                FormId = 1,
                OrderId = 4,
                ItemType = (int)ItemType.DateTime,
                ItemTitle = "請假起日",
                ItemValue = "BeginDateTime",
                ItemDesc = null,
                ItemFormat = null,
                DataRef = null,
                Multiple = null,
                Disabled = null,
                Required = "Y"
            },
            new FormItemVM(){
                Id = 5,
                FormId = 1,
                OrderId = 5,
                ItemType = (int)ItemType.DateTime,
                ItemTitle = "請假起日",
                ItemValue = "EndDateTime",
                ItemDesc = null,
                ItemFormat = null,
                DataRef = null,
                Multiple = null,
                Disabled = null,
                Required = "Y"
            },
            new FormItemVM(){
                Id = 6,
                FormId = 1,
                OrderId = 6,
                ItemType = (int)ItemType.Select,
                ItemTitle = "代理人",
                ItemValue = "AgentNo",
                ItemDesc = null,
                ItemFormat = null,
                DataRef = "LeaveAgentOption",
                Multiple = null,
                Disabled = null,
                Required = "Y"
            }
        };
    }
}
