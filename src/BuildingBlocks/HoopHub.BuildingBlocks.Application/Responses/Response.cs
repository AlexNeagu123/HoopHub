namespace HoopHub.BuildingBlocks.Application.Responses
{
    public class Response<T> : BaseResponse
    {
        public Response() : base()
        {

        }
        public T Data { get; set; } = default!;
    }
}
