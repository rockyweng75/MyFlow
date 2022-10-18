
using MyFlow.Data.DAOs;
using MyFlow.Data.DAOs.Basic;
using MyFlow.Data.Models;
using MyFlow.Domain.Models;

namespace MyFlow.Service.Impl
{
    public class SwitchService : BasicCRUDService<SwitchDao, Switch, SwitchVM>
    {
        private ISwitchDao switchDao;

        public override BasicDao<Switch> dao {
            get{
                return (BasicDao<Switch>)switchDao;
            }
        }

        public SwitchService(ISwitchDao switchDao)
        {
            this.switchDao = switchDao;
        }
    }
}
