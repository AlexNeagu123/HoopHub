using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;
using HoopHub.Modules.UserFeatures.Domain.FanNotifications;

namespace HoopHub.Modules.UserFeatures.Application.FanNotifications.Dtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public FanDto Recipient { get; set; } = null!;
        public FanDto? Sender { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? AttachedImageUrl { get; set; }
    }
}
