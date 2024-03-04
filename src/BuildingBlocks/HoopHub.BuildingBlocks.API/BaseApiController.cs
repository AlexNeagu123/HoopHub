using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace HoopHub.BuildingBlocks.API
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ISender _mediator = null!;
        protected ISender Mediator => _mediator
            ??= HttpContext.RequestServices
                .GetRequiredService<ISender>();
    }
}
