using job_app_management_system.api.Models;

namespace job_app_management_system.api.Result
{
    public class Result<T>
    {

        public bool IsError { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }

        public Result() { }

        public Result(bool isError, List<string> messages, T data)
        {
            IsError = isError;
            Messages = messages;
            Data = data;
        }
    }

}
