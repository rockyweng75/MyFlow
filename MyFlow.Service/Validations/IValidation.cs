using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Validations
{
    public interface IValidation
    {
        string Name { get; }
        Task<string> Invoke(FlowchartVM flowchart, StageVM stage, dynamic formData, UserInfoVM user);
    }
}