using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Domain.Fans
{
    public class Fan
    {
        public string Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string? AvatarPhotoUrl { get; private set; }
        public ICollection<TeamThread> TeamThreads { get; private set; } = null!;
        public ICollection<ThreadComment> Comments { get; private set; } = [];
        public ICollection<CommentVote> Votes { get; private set; } = [];

        private Fan(string id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }

        public static Result<Fan> Create(string id, string username, string email)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Result<Fan>.Failure(ValidationErrors.InvalidFanId);
            if (string.IsNullOrWhiteSpace(username))
                return Result<Fan>.Failure(ValidationErrors.InvalidUsername);
            if (string.IsNullOrWhiteSpace(email))
                return Result<Fan>.Failure(ValidationErrors.InvalidEmail);

            return Result<Fan>.Success(new Fan(id, username, email));
        }

        public void UpdateAvatarPhotoUrl(string avatarPhotoUrl)
        {
            AvatarPhotoUrl = avatarPhotoUrl;
        }
    }
}
