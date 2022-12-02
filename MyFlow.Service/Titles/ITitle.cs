using MyFlow.Domain.Enums;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Titles
{
    public interface ITitle
    {
        public string Name { get; }

        public Task<string> Invoke(ApplyDataVM applyData);
    }
}