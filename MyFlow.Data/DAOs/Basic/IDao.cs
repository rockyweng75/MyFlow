using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Data.DAOs.Basic
{
    public interface IDao<TEntity> : IDisposable
    {
        Task<TEntity?> Get(int Id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetList(TEntity entity);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetListQueryable(TEntity entity);
        
        Task<TEntity> Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveChangesAsync();
    }
}
