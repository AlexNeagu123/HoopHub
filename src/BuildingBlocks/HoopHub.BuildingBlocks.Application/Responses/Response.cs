using FluentValidation.Results;
using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.BuildingBlocks.Application.Responses
{
    public class Response<T> : BaseResponse where T : class
    {
        public Response() : base()
        {

        }
        public T Data { get; set; } = default!;

        public static Response<T> ErrorResponseFromKeyMessage(string errorMessage, string errorKey)
        {
            return new Response<T>
            {
                Success = false,
                ValidationErrors = new Dictionary<string, string> { { errorKey, errorMessage} }
            };
        }

        public static Response<T> ErrorResponseFromFluentResult(ValidationResult result)
        {
            return new Response<T>
            {
                Success = false,
                ValidationErrors = result.Errors.Take(1).ToDictionary(error => error.PropertyName, error => error.ErrorMessage)
            };
        }
    }
}
