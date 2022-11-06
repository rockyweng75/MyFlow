using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Template.Tt.Controller
{
    public partial class CRUDControllerTemplate
    {

        private const string TAB = "\t";

        public string ControllerName = "123456";

        public string ServiceName = "123456";

        public string ViewModelName = "123456";


        public CRUDControllerTemplate(string ViewModelName)
        {
            this.ViewModelName = ViewModelName;
            var _ViewModelName = ViewModelName.Replace("VM", "");
            ControllerName = $"{_ViewModelName}Controller";
            ServiceName = $"{_ViewModelName}Service";

        }
    }
}
