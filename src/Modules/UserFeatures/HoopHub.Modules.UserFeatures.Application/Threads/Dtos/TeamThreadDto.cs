﻿using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;

namespace HoopHub.Modules.UserFeatures.Application.Threads.Dtos
{
    public class TeamThreadDto
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public FanDto? Fan { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public DateTime CreatedDate { get; set; }
        public VoteStatus? VoteStatus { get; set; }
        public int CommentsCount { get; set; }
    }
}
