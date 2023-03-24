namespace Sample.Models
{
    public class BaseResponse<T>
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public T result { get; set; }
    }
}
