using FluentValidation.Results;

namespace HoopHub.BuildingBlocks.Application.Responses
{
    public class PagedResponse<T> : BaseResponse where T : class
    {
        public PagedResponse() : base()
        {

        }
        public T Data { get; set; } = default!;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((decimal)TotalRecords / PageSize);
        public int TotalRecords { get; set; }

        public new static PagedResponse<T> ErrorResponseFromKeyMessage(string errorMessage, string errorKey)
        {
            return new PagedResponse<T>
            {
                Success = false,
                ValidationErrors = new Dictionary<string, string> { { errorKey, errorMessage } }
            };
        }

        public new static PagedResponse<T> ErrorResponseFromFluentResult(ValidationResult result)
        {
            return new PagedResponse<T>
            {
                Success = false,
                ValidationErrors = result.Errors.Take(1).ToDictionary(error => error.PropertyName, error => error.ErrorMessage)
            };
        }
    }
}
