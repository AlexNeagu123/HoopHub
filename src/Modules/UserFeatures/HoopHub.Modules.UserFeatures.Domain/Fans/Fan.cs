using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.FanNotifications;
using HoopHub.Modules.UserFeatures.Domain.Follows;
using HoopHub.Modules.UserFeatures.Domain.Reviews;
using HoopHub.Modules.UserFeatures.Domain.Rules;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Domain.Fans
{
    public class Fan : AuditableEntity
    {
        public string Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public int UpVotes { get; private set; }
        public int DownVotes { get; private set; }
        public FanBadgeType FanBadge { get; private set; }
        public string? AvatarPhotoUrl { get; private set; } = Config.DefaultAvatarPhotoUrl;
        public Guid? FavouriteTeamId { get; private set; }
        public ICollection<TeamThread> TeamThreads { get; private set; } = [];
        public ICollection<ThreadComment> Comments { get; private set; } = [];
        public ICollection<CommentVote> Votes { get; private set; } = [];
        public ICollection<GameReview> GameReviews { get; private set; } = [];
        public ICollection<PlayerPerformanceReview> PlayerPerformanceReviews { get; private set; } = [];
        public ICollection<TeamFollowEntry> TeamFollowEntries { get; private set; } = [];
        public ICollection<PlayerFollowEntry> PlayerFollowEntries { get; private set; } = [];
        public ICollection<Notification> NotificationsSent { get; private set; } = [];
        public ICollection<Notification> NotificationsReceived { get; private set; } = [];

        private Fan(string id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
            FanBadge = FanBadgeType.Rookie;
        }

        public static Result<Fan> Create(string id, string username, string email)
        {
            try
            {
                CheckRule(new FanIdCannotBeEmpty(id));
                CheckRule(new FanUsernameCannotBeEmpty(username));
                CheckRule(new FanEmailCannotBeEmpty(email));
            }
            catch (BusinessRuleValidationException ex)
            {
                return Result<Fan>.Failure(ex.Details);
            }

            return Result<Fan>.Success(new Fan(id, username, email));
        }

        public void UpdateAvatarPhotoUrl(string avatarPhotoUrl)
        {
            try
            {
                CheckRule(new ImageUrlCannotBeEmpty(avatarPhotoUrl));
                AvatarPhotoUrl = avatarPhotoUrl;
            }
            catch (BusinessRuleValidationException) { }
        }

        public void UpdateFavouriteTeamId(Guid? favouriteTeamId)
        {
            FavouriteTeamId = favouriteTeamId;
        }

        public void UpVote()
        {
            UpVotes++;
            CheckBadgeUpdate();
        }

        public void DownVote()
        {
            DownVotes++;
            CheckBadgeUpdate();
        }

        public void RemoveVote(bool isUpVote)
        {
            if (isUpVote)
                UpVotes--;
            else
                DownVotes--;

            CheckBadgeUpdate();
        }

        public void UpdateVoteCount(bool isUpVote)
        {
            if (isUpVote)
            {
                UpVotes++;
                DownVotes--;
            }
            else
            {
                UpVotes--;
                DownVotes++;
            }

            CheckBadgeUpdate();
        }

        private void CheckBadgeUpdate()
        {
            FanBadge = UpVotes switch
            {
                < Config.RookieBadgeThreshold => FanBadgeType.Rookie,
                < Config.RegularBadgeThreshold => FanBadgeType.Regular,
                < Config.ExpertBadgeThreshold => FanBadgeType.Expert,
                _ => FanBadgeType.Goat
            };
        }
    }
}
