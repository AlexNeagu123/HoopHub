using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using Riok.Mapperly.Abstractions;

namespace HoopHub.Modules.UserFeatures.Application.Fans.Mappers
{
    [Mapper]
    public partial class FanMapper
    {
        public partial FanDto FanToFanDto(Fan fan);
    }
}
