using FluentValidation.Results;

namespace HoopHub.BuildingBlocks.Application.Responses
{
    public class BaseResponse
    {
        public BaseResponse() => Success = true;
        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public Dictionary<string, string> ValidationErrors { get; set; }

        public static BaseResponse ErrorResponseFromKeyMessage(string errorMessage, string errorKey)
        {
            return new BaseResponse
            {
                Success = false,
                ValidationErrors = new Dictionary<string, string> { { errorKey, errorMessage } }
            };
        }

        public static BaseResponse ErrorResponseFromFluentResult(ValidationResult result)
        {
            return new BaseResponse
            {
                Success = false,
                ValidationErrors = result.Errors.Take(1).ToDictionary(error => error.PropertyName, error => error.ErrorMessage)
            };
        }
    }
}
