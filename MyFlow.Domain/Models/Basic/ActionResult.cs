namespace MyFlow.Domain.Models.Basic
{
    public class ActionResult 
    {
        public ActionResult() { }

        public ActionResult(bool Success, string Msg = "")
        {
            this.Success = Success;
            this.Msg = Msg;
        }
        public bool Success { get; set; }
        public string? Msg { get; set; }
        public IList<string>? Msgs { get; set; }
    }
}