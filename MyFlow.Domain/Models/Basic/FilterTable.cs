namespace MyFlow.Domain.Models.Basic
{

    public interface IFilterTable
    {
        public string? CurrentFilter { get; set; }

        public IDictionary<string, Filter> Filter { get; set; }

    }
    public class FilterTable : IFilterTable
    {
        public string? CurrentFilter { get; set; }

        public IDictionary<string, Filter> Filter { get; set; }
            = new Dictionary<string, Filter>();
    }

    public class Filter 
    {
        public string? Column { get; set; }

        public dynamic? Values { get; set; }

        public IList<Object>? _Values { get; }

        public dynamic? Selected { get; set; }

        public IList<string> _Selected 
        {
            get 
            {
                var result = new List<string>();
                if(this.Selected == null) return result;
                var temp = 
                    Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, object>>(this.Selected!.ToString());
                foreach (var value in temp.Values) 
                {
                    result.Add(value.value.ToString());
                }
                return result;
            }
        }
    }
}
