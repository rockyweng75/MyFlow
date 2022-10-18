using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Data.Models
{
    public partial class Test : IDataModel
    {
        public int Id { get; set; }
        public string Column1 { get; set; }
        public int? Column2 { get; set; }
        public DateTime? Column3 { get; set; }
    }
}
