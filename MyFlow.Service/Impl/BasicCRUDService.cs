using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public abstract class BasicCRUDService<TDao, TDataModel, TViewModel> : IService
        where TDao : BasicDao<TDataModel>
        where TDataModel : class, new()
        where TViewModel : class, new()
    {
        public abstract BasicDao<TDataModel> dao { get; }

        public async Task<TViewModel> Get(int Id)
        {
            IDataModel result = (IDataModel) await dao.Get(Id);
            return result != null ? result.ToViewModel<TViewModel>() : null;
        }

        public virtual async Task<IList<TViewModel>> GetList(TViewModel vm)
        {
            IViewModel _vm = (IViewModel)vm;
            TDataModel dataModel = _vm.ToDataModel<TDataModel>();
            var task = await dao.GetList(dataModel);
            
            var result = task
                .Cast<IDataModel>()
                .Select(o => o.ToViewModel<TViewModel>())
                .ToList();

            return result;
        }

        public virtual async Task<int> Create(TViewModel vm)
        {
            IViewModel _vm = (IViewModel)vm;
            TDataModel dataModel = _vm.ToDataModel<TDataModel>();
            var result = await dao.Create(dataModel);
            await dao.SaveChangesAsync();
            return ((IDataModel)result).Id;
        }

        public virtual async Task Update(TViewModel vm)
        {
            IViewModel _vm = (IViewModel)vm;
            TDataModel dataModel = _vm.ToDataModel<TDataModel>();
            dao.Update(dataModel);
            await dao.SaveChangesAsync();
        }

        public async Task Delete(TViewModel vm)
        {
            IViewModel _vm = (IViewModel)vm;
            TDataModel dataModel = _vm.ToDataModel<TDataModel>();
            dao.Delete(dataModel);
            await dao.SaveChangesAsync();
        }

    }
}
