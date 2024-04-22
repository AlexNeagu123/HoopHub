using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.FanNotifications.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.FanNotifications.GetNotifications
{
    public class GetNotificationsQuery : IRequest<PagedResponse<IReadOnlyList<NotificationDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool OnlyUnread { get; set; } = true;
    }
}
