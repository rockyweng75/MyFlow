using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class TestService : BasicCRUDService<TestDao, Test, TestVM>
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
