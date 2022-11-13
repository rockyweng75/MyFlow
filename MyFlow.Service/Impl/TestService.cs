
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{

    public interface ITestService : IBasicCRUDService<TestVM> 
    { 
    }

    public class TestService : BasicCRUDService<TestDao, Test, TestVM>, ITestService
    {
        private ITestDao testDao;

        public override BasicDao<Test> dao {
            get{
                return (BasicDao<Test>)testDao;
            }
        }

        public TestService(ITestDao testDao)
        {
            this.testDao = testDao;
        }
    }
}
