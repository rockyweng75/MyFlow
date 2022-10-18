using MyFlow.Data.Models;
using MyFlow.Domain.Models;
using MyFlow.Domain.Tools;

namespace MyFlow.Service
{
    public static class Expansion
    {

        public static TEntity ToDataModel<TEntity>(this IViewModel viewModel)
        {
            var entityType = typeof(TEntity);
            TEntity result = (TEntity)Activator.CreateInstance(entityType);
            ObjectClone.Clone(viewModel, result);
            return result;
        }

        public static TEntity ToViewModel<TEntity>(this IDataModel viewModel)
        {
            var entityType = typeof(TEntity);
            TEntity result = (TEntity)Activator.CreateInstance(entityType);
            ObjectClone.Clone(viewModel, result);
            return result;
        }

        public static void Copy(this IViewModel viewModel, object target)
        {
            ObjectClone.Clone(viewModel, target);
        }

        public static void Copy(this IDataModel dataModel, object target)
        {
            ObjectClone.Clone(dataModel, target);
        }

    }
}