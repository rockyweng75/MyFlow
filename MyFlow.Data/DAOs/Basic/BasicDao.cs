using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MyFlow.Data.DAOs.Basic
{
    public class BasicDao<TEntity> : IDao<TEntity> where TEntity : class, new()
    {
        private DbContext? dbContext;

        public BasicDao()
        {
        }

        public BasicDao(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string GetTableName()
        {
            var entityType = typeof(TEntity);
            var tableName = entityType.Name;
            if (entityType.GetCustomAttributes(true).Any(o => o.GetType() == typeof(TableAttribute)))
            {
                var tableAttribute = (TableAttribute)entityType.GetCustomAttributes(true)
                    .Where(o => o.GetType() == typeof(TableAttribute));

                tableName = tableAttribute.Name;
            }
            return tableName;
        }

        public async Task<TEntity?> Get(int Id)
        {
            var dbSet = dbContext!.Set<TEntity>();
            var tableName = GetTableName();
            var _Id = new SqlParameter("Id", Id);
            var queryable = dbSet.FromSqlRaw($"select * from {tableName} where Id = @Id", _Id);
            return await queryable.SingleOrDefaultAsync();
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            var dbSet = dbContext!.Set<TEntity>();
            return Task.FromResult(dbSet.AsEnumerable());
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            var dbSet = dbContext!.Set<TEntity>();
            return dbSet.Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> GetListQueryable(TEntity entity)
        {
            var dbSet = dbContext!.Set<TEntity>();
            var entityType = typeof(TEntity);
            var props = entityType.GetProperties();
            ParameterExpression parameterExpression = Expression.Parameter(entityType);
            foreach (var prop in props)
            {
                var value = prop.GetValue(entity, null);
                if (value != null)
                {
                    Expression memberExpression = Expression.Property(parameterExpression, prop);
                    Expression? valueExpression = null;
                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var filter1 =
                           Expression.Constant(
                               Convert.ChangeType(value, memberExpression.Type.GetGenericArguments()[0]));
                        valueExpression = Expression.Convert(filter1, memberExpression.Type);
                    }
                    else 
                    { 
                        valueExpression = Expression.Constant(value);
                    }
                    Expression equalityExpression = Expression.Equal(memberExpression, valueExpression);
                    Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equalityExpression, parameterExpression);

                    dbSet.Where(lambda);
                }
            }

            return dbSet.AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetList(TEntity entity)
        {
            var dbSet = dbContext!.Set<TEntity>();
            var entityType = typeof(TEntity);
            var props = entityType.GetProperties();
            ParameterExpression parameterExpression = Expression.Parameter(entityType);
            foreach (var prop in props)
            {
                var value = prop.GetValue(entity, null);
                if (value != null)
                {
                    Expression memberExpression = Expression.Property(parameterExpression, prop);
                    Expression? valueExpression = null;
                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var filter1 =
                           Expression.Constant(
                               Convert.ChangeType(value, memberExpression.Type.GetGenericArguments()[0]));
                        valueExpression = Expression.Convert(filter1, memberExpression.Type);
                    }
                    else 
                    { 
                        valueExpression = Expression.Constant(value);
                    }
                    Expression equalityExpression = Expression.Equal(memberExpression, valueExpression);
                    Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equalityExpression, parameterExpression);

                    dbSet.Where(lambda);
                }
            }

            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var dbSet = dbContext!.Set<TEntity>();
            await dbSet.AddAsync(entity);
            Debug.WriteLine(dbSet.ToQueryString());
            return entity;
        }

        public void Update(TEntity entity)
        {
            
            dbContext!.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            dbContext!.Entry(entity).State = EntityState.Deleted;
            //var dbSet = dbContext.Set<TEntity>();
            //dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext!.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext!.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
