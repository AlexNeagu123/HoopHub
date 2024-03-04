public class Result<T> where T : class
{
    protected Result(bool isSuccess, T value, string errorMsg)
    {
        isSuccess = isSuccess;
        Value = value;
        ErrorMsg = errorMsg;
    }

    public bool IsSuccess { get; }
    public T Value { get; }
    public string ErrorMsg { get; }
    public static Result<T> Success(T value) => new Result<T>(true, value, null);
    public static Result<T> Failure(string errorMsg) => new Result<T>(false, null, errorMsg);
}