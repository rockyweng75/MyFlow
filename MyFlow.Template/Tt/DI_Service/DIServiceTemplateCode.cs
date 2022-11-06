using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Template.Tt.DI_Service
{
    public partial class DIServiceTemplate
    {

        private const string TAB = "\t";

        public IList<string> Services = new List<string>();

        public DIServiceTemplate(IList<string> Services)
        {
            this.Services = Services;
        }
    }
}
