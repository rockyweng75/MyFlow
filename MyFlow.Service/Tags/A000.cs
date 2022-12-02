using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Tags
{
    public class A000: ITag 
    {
        public string Name => "{流程名稱},{申請人}";

        public Task<string> Invoke(ApplyDataVM applyData){
            var applyName = string.IsNullOrEmpty(applyData.ApplyName);
            return Task.FromResult($"{applyName},{applyData.FlowName}");
        }
    }
}