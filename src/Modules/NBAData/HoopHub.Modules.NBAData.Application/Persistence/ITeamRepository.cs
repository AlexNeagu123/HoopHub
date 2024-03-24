﻿using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface ITeamRepository : IAsyncRepository<Team>
    {
        Task<Result<Team>> FindByIdAsyncIncludingPlayers(Guid id);
        Task<Result<Team>> FindByIdAsyncIncludingBio(Guid id);
        Task<Result<Team>> FindByApiIdAsync(int apiId);
    }
}
