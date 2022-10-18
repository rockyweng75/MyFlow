using Microsoft.EntityFrameworkCore;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;

namespace MyFlow.Data.DAOs
{
    public interface ITestDao : IDao<Test>
    {

    }
    public class TestDao : BasicDao<Test>, ITestDao
    {
        public TestDao(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
