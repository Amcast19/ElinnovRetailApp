namespace ElinnovRetail.Models.Common
{
    public class ResultValue
    {
        public ResultValue(string message = "", bool sucess = false, object data = null)
        {
            Success = sucess;
            Message = message;
            ReturnData = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object ReturnData { get; set; }
    }

    public class ResultValue<T> where T : class, new()
    {
        public ResultValue(string message, bool success = false, T data = null)
        {
            Success = success;
            Message = message;
            ReturnData = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public T ReturnData { get; set; }
    }
}
