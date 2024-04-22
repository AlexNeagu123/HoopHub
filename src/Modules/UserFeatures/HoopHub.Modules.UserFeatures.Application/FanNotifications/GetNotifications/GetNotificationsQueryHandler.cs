using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.FanNotifications.Dtos;
using HoopHub.Modules.UserFeatures.Application.FanNotifications.Mappers;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.FanNotifications.GetNotifications
{
    public class GetNotificationsQueryHandler(INotificationRepository notificationRepository, ICurrentUserService userService)
        : IRequestHandler<GetNotificationsQuery, PagedResponse<IReadOnlyList<NotificationDto>>>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly NotificationMapper _notificationMapper = new();


        public async Task<PagedResponse<IReadOnlyList<NotificationDto>>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new GetNotificationsQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return PagedResponse<IReadOnlyList<NotificationDto>>.ErrorResponseFromFluentResult(validationResult);

            var notifications = request.OnlyUnread ? await _notificationRepository.GetByFanIdAndIsReadFlagPagedAsync(fanId!, request.Page, request.PageSize, false)
                : await _notificationRepository.GetByFanIdPagedAsync(fanId!, request.Page, request.PageSize);

            if (!notifications.IsSuccess)
                return PagedResponse<IReadOnlyList<NotificationDto>>.ErrorResponseFromKeyMessage(notifications.ErrorMsg, ValidationKeys.Notification);

            var notificationDtoList = notifications.Value.Select(notification => _notificationMapper.NotificationToNotificationDto(notification)).ToList();
            return new PagedResponse<IReadOnlyList<NotificationDto>>
            {
                Success = true,
                Data = notificationDtoList,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalRecords = notifications.TotalCount
            };
        }
    }
}
