namespace MyFlow.Service.Impl
{
    public interface IBasicCRUDService<TViewModel> : IService where TViewModel : class, new() 
    {
        Task<int> Create(TViewModel vm);
        Task Delete(int Id);
        Task<TViewModel> Get(int Id);
        Task<IList<TViewModel>> GetList(TViewModel vm);
        Task Update(TViewModel vm);
    }
}