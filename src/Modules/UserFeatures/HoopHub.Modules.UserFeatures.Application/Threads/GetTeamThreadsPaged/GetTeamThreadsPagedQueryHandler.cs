using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadsPaged
{
    public class GetTeamThreadsPagedQueryHandler(ITeamThreadRepository teamThreadRepository) : IRequestHandler<GetTeamThreadsPagedQuery, PagedResponse<ICollection<TeamThreadDto>>>
    {
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly TeamThreadMapper _teamThreadMapper = new();
        public async Task<PagedResponse<ICollection<TeamThreadDto>>> Handle(GetTeamThreadsPagedQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTeamThreadsPagedQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
                return PagedResponse<ICollection<TeamThreadDto>>.ErrorResponseFromFluentResult(validationResult);

            var threadsResult = await _teamThreadRepository.GetByTeamIdPagedAsync(request.TeamId, request.Page, request.PageSize);
            if(!threadsResult.IsSuccess)
                return PagedResponse<ICollection<TeamThreadDto>>.ErrorResponseFromKeyMessage(threadsResult.ErrorMsg, ValidationKeys.TeamThread);

            var threads = threadsResult.Value;
            var threadsDto = threads.Select(thread => _teamThreadMapper.TeamThreadToTeamThreadDto(thread)).ToList();
            
            return new PagedResponse<ICollection<TeamThreadDto>> {
                Success = true,
                Data = threadsDto, 
                Page = request.Page,
                PageSize = request.PageSize,
                TotalRecords = threadsResult.TotalCount,
            };
        }
    }
}
