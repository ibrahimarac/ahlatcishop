namespace Ahlatci.Shop.UI.Models.Wrapper
{
    public class Result<T> where T:new()
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
