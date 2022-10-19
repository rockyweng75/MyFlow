using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Template.Tt
{
    public partial class DIDaoTemplate
    {

        private const string TAB = "\t";

        public IList<string> Daos = new List<string>();

        public DIDaoTemplate(IList<string> Daos)
        {
            this.Daos = Daos;
        }
    }
}
