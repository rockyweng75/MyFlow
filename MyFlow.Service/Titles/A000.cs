using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Titles
{
    public class A000: ITitle 
    {
        public string Name => "{申請人}申請{表單名稱}";

        public Task<string> Invoke(ApplyDataVM applyData){
            var applyName = string.IsNullOrEmpty(applyData.ApplyName) ?
                    applyData.Id.ToString() : 
                    applyData.ApplyName;
            return Task.FromResult($"{applyName}申請{applyData.FlowName}");
        }
    }
}