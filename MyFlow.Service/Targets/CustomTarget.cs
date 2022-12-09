using MyFlow.Domain.Models;

namespace MyFlow.Service.Targets
{
    public class CustomTarget : ITarget
    {
        public string Name => "自訂";

        public Task<string> Invoke(StageVM stage, ApplyDataVM? applyData, ApproveDataVM? approveDataVM)
        {
            if (approveDataVM == null)
            {
                return applyData!.DynamicFormData != null ? 
                    applyData.DynamicFormData[stage.TargetParams] :
                    throw new Exception("取得簽核者發生錯誤：找不到申請資料");
            }
            else 
            {
                return approveDataVM.DynamicFormData != null ?
                    approveDataVM.DynamicFormData[stage.TargetParams] :
                    throw new Exception("取得簽核者發生錯誤：找不到簽核資料");
            }
        }
    }
}
