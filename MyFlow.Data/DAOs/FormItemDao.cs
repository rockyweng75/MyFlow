using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IFormItemDao : IDao<FormItem> { 
        Task<IList<FormItem>> GetList(Flowchart flowchart);
        Task<IList<FormItem>> GetList(ActionForm actionForm);
    } 
    public class FormItemDao : BasicDao<FormItem>, IFormItemDao
    {
        private DbContext dbContext;
        public FormItemDao(DbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<FormItem>> GetList(Flowchart flowchart)
        {
            var queryable =
                    dbContext.Set<FormItem>().FromSqlInterpolated(
                    $@"
                        select a.* 
                        from FormItem a
                        where exists (
                            select 1 from Flowchart f 
                            join Stage s on f.Id = s.FlowId 
                            join ActionForm af on af.StageId = s.Id
                            where f.Id = { flowchart.Id }
                            and af.FormId = a.FormId
                        )
                    ");
            return await queryable.ToListAsync();
        }

        public async Task<IList<FormItem>> GetList(ActionForm actionForm)
        {
            var queryable =
                    dbContext.Set<FormItem>()
                    .Where(o => o.FormId == actionForm.Id)
                    .AsQueryable();
            return await queryable.ToListAsync();
        }
    }
}
