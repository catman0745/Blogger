namespace Catman.Blogger.Core.Services.Common
{
    public abstract class Service
    {
        protected static Response<T> Success<T>(T value)
        {
            return new Response<T>(
                success: true,
                result: value,
                errorMessage: default);
        }

        protected static Response<T> Failure<T>(string errorMessage)
        {
            return new Response<T>(
                success: false,
                result: default,
                errorMessage: errorMessage);
        }
    }
}
