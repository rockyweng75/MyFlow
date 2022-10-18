using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Template.Tt
{
    public partial class CRUDServiceTemplate
    {

        private const string TAB = "\t";

        public string DataModel = "123456";

        public CRUDServiceTemplate(string DataModelName)
        {
            DataModel = DataModelName;
        }
    }
}
