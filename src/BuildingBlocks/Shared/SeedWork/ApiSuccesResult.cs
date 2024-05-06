namespace Shared.SeedWork
{
    public class ApiSuccesResult<T> : ApiResult<T>
    {
        public ApiSuccesResult(T data) : base(true, data, "Success")
        {
        }

        public ApiSuccesResult(T data, string message) : base(true, data, message)
        {
        }
    }
}
