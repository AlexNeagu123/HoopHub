using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadsByUserPaged
{
    public class GetTeamThreadsByUserPagedQueryHandler(
        ITeamThreadRepository teamThreadRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetTeamThreadsByUserPagedQuery, PagedResponse<ICollection<TeamThreadDto>>>
    {
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly TeamThreadMapper _teamThreadMapper = new();

        public async Task<PagedResponse<ICollection<TeamThreadDto>>> Handle(GetTeamThreadsByUserPagedQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTeamThreadsByUserPagedQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return PagedResponse<ICollection<TeamThreadDto>>.ErrorResponseFromFluentResult(validationResult);

            var fanId = request.FanId ?? _currentUserService.GetUserId;
            var threadsResult = await _teamThreadRepository.GetByFanIdPagedAsync(fanId!, request.Page, request.PageSize);
            if (!threadsResult.IsSuccess)
                return PagedResponse<ICollection<TeamThreadDto>>.ErrorResponseFromKeyMessage(threadsResult.ErrorMsg, ValidationKeys.TeamThread);

            var threads = threadsResult.Value;
            var threadsDto = threads.Select(thread => _teamThreadMapper.TeamThreadToTeamThreadDto(thread)).ToList();

            return new PagedResponse<ICollection<TeamThreadDto>>
            {
                Success = true,
                Data = threadsDto,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalRecords = threadsResult.TotalCount,
            };
        }
    }
}
