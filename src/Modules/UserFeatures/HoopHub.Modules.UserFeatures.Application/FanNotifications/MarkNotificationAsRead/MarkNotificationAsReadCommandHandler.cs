using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.FanNotifications.Dtos;
using HoopHub.Modules.UserFeatures.Application.FanNotifications.Mappers;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.FanNotifications.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommandHandler(INotificationRepository notificationRepository, ICurrentUserService userService) : IRequestHandler<MarkNotificationAsReadCommand, Response<NotificationDto>>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly NotificationMapper _notificationMapper = new();

        public async Task<Response<NotificationDto>> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var validator = new MarkNotificationAsReadCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<NotificationDto>.ErrorResponseFromFluentResult(validationResult);

            var notification = await _notificationRepository.FindByIdAsync(request.Id);
            if (!notification.IsSuccess)
                return Response<NotificationDto>.ErrorResponseFromKeyMessage(notification.ErrorMsg, ValidationKeys.Notification);

            var fanId = _userService.GetUserId;
            if (notification.Value.RecipientId != fanId)
                return Response<NotificationDto>.ErrorResponseFromKeyMessage(ValidationErrors.NotificationNotRecipient, ValidationKeys.Notification);

            notification.Value.MarkAsRead();
            var updatedNotification = await _notificationRepository.UpdateAsync(notification.Value);
            if (!updatedNotification.IsSuccess)
                return Response<NotificationDto>.ErrorResponseFromKeyMessage(updatedNotification.ErrorMsg, ValidationKeys.Notification);

            return new Response<NotificationDto>
            {
                Success = true,
                Data = _notificationMapper.NotificationToNotificationDto(updatedNotification.Value)
            };
        }
    }
}
