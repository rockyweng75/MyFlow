using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models.Basic;

namespace MyFlow.Service.Impl
{
    public abstract class BasicCRUDService<TDao, TDataModel, TViewModel> : IBasicCRUDService<TViewModel>
        where TDao : BasicDao<TDataModel> 
        where TDataModel : class, IDataModel, new()
        where TViewModel : class, new()
    {
        public abstract BasicDao<TDataModel> dao { get; }

        public async Task<TViewModel?> Get(int Id)
        {
            IDataModel? result = (IDataModel)(await dao.Get(Id))!;
            return result is not null ? ToViewModel(result): null;
        }

        public virtual async Task<IList<TViewModel>> GetList(TViewModel vm)
        {
            IViewModel _vm = (IViewModel)vm;
            TDataModel dataModel = ToDataModel(_vm);
            var task = await dao.GetList(dataModel);

            var result = task
                .Cast<IDataModel>()
                .Select(o => ToViewModel(o))
                .ToList();

            return result;
        }

        public virtual async Task<int> Create(TViewModel vm)
        {
            IViewModel _vm = (IViewModel)vm;
            TDataModel dataModel = ToDataModel(_vm);
            var result = await dao.Create(dataModel);
            await dao.SaveChangesAsync();
            return ((IDataModel)result).Id;
        }

        public virtual async Task Update(TViewModel vm)
        {
            IViewModel _vm = (IViewModel)vm;
            TDataModel dataModel = ToDataModel(_vm);
            dao.Update(dataModel);
            await dao.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            TDataModel dataModel =(await dao.Get(Id))!;
            dao.Delete(dataModel);
            await dao.SaveChangesAsync();
        }

        public virtual TViewModel ToViewModel(IDataModel dataModel)
        {
            return dataModel.ToViewModel<TViewModel>();
        }

        public virtual TDataModel ToDataModel(IViewModel viewModel)
        {
            return viewModel.ToDataModel<TDataModel>();
        }
    }
}
