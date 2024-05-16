using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.FanNotifications.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.FanNotifications.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommand : IRequest<Response<NotificationDto>>
    {
        public Guid Id { get; set; }
    }
}
