namespace Catman.Blogger.Core.Services.Common
{
    public class Response<T>
    {
        public bool Success { get; }
        
        public T Result { get; }
        
        public string ErrorMessage { get; }

        public Response(bool success, T result, string errorMessage)
        {
            Success = success;
            Result = result;
            ErrorMessage = errorMessage ?? string.Empty;
        }
    }
}
