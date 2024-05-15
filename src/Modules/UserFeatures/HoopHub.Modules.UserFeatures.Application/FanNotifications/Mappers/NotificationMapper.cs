using HoopHub.Modules.UserFeatures.Application.FanNotifications.Dtos;
using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Domain.FanNotifications;

namespace HoopHub.Modules.UserFeatures.Application.FanNotifications.Mappers
{
    public class NotificationMapper
    {
        private readonly FanMapper _fanMapper = new();

        public NotificationDto NotificationToNotificationDto(Notification notification)
        {
            return new NotificationDto
            {
                Id = notification.Id,
                Sender = notification.Sender != null ? _fanMapper.FanToFanDto(notification.Sender) : null,
                Recipient = _fanMapper.FanToFanDto(notification.Recipient),
                Title = notification.Title,
                Content = notification.Content,
                AttachedImageUrl = notification.AttachedImageUrl,
                IsRead = notification.IsRead,
                Type = notification.Type,
                CreatedDate = notification.CreatedDate
            };
        }
    }
}
