using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Players.Dtos;
using HoopHub.Modules.NBAData.Domain.Players;

namespace HoopHub.Modules.NBAData.Application.Players.Mappers
{
    public class PlayerMapper
    {
        public PlayerDto PlayerToPlayerDto(Player player, bool isLicensed)
        {
            return new PlayerDto
            {
                Id = player.Id,
                ApiId = player.ApiId,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Position = player.Position,
                Height = player.Height,
                ImageUrl = isLicensed ? player.ImageUrl : Config.DefaultPlayerImageUrl,
                Weight = player.Weight,
                JerseyNumber = player.JerseyNumber,
                College = player.College,
                Country = player.Country,
                DraftYear = player.DraftYear,
                DraftRound = player.DraftRound,
                DraftNumber = player.DraftNumber,
                IsActive = player.IsActive,
                AverageRating = player.AverageRating,
                CurrentTeamId = player.CurrentTeamId,
            };
        }
    }
}
