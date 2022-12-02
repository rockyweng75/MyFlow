using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Tags
{
    public interface ITag
    {
        public string Name { get; }

        public Task<string> Invoke(ApplyDataVM applyData);
    }
}