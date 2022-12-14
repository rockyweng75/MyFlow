using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface IFormDao : IDao<Form> { 
    
    } 
    public class FormDao : BasicDao<Form>, IFormDao
    {

        private DbContext dbContext;
        public FormDao(DbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        // public async Task<IList<FormItem>> GetList(Flowchart flowchart)
        // {
        //     var queryable =
        //             dbContext.Set<FormItem>().FromSqlInterpolated(
        //             $@"
        //                 select a.* 
        //                 from FormItem a
        //                 where exists (
        //                     select 1 from Flowchart f 
        //                     join Stage s on f.Id = s.FlowId 
        //                     join ActionForm af on af.StageId = s.Id
        //                     where f.Id = { flowchart.Id }
        //                     and af.FormId = a.FormId
        //                 )
        //             ");
        //     return await queryable.ToListAsync();
        // }
    }
}
