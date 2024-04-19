namespace HoopHub.BuildingBlocks.Domain
{
    public class PagedResult<T> : Result<T> where T : class
    {
        public int TotalCount { get; private set; }
        protected PagedResult(bool isSuccess, T value, string errorMsg, int totalCount) : base(isSuccess, value, errorMsg)
        {
            TotalCount = totalCount;
        }
        public static PagedResult<T> Success(T value, int totalCount)
        {
            return new PagedResult<T>(true, value, null!, totalCount);
        }
        public new static PagedResult<T> Failure(string errorMsg)
        {
            return new PagedResult<T>(false, null!, errorMsg, default);
        }
    }
}
