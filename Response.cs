namespace MappingApp
{
    public class Response<T>
    {
        public T Values { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public Response(T values, string status, string message)
        {
            Values = values;
            Status = status;
            Message = message;
        }
    }
}
