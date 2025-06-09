namespace FPMG.UserService.WebAPIs.ResponseType
{
    public class JsonResponse<T>
    {
        public JsonResponse(T data, int status, string message)
        {
            Value = new Value<T>
            {
                Message = message,
                Status = status,
                Data = data
            };
        }
        public Value<T> Value { get; set; }
    }

    public class Value<T>
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public T Data { get; set; }
    }
}
