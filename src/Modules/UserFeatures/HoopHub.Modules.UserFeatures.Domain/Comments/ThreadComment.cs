using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Domain.Comments
{
    public class ThreadComment : AuditableEntity, ISoftDeletable
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid? ParentId { get; private set; }
        public string? RespondsToId { get; private set; }
        public Fan? RespondsTo { get; private set; }
        public string Content { get; private set; }
        public Guid? TeamThreadId { get; private set; }
        public TeamThread? TeamThread { get; private set; }

        public Guid? GameThreadId { get; private set; }
        public GameThread? GameThread { get; private set; }

        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public int UpVotes { get; private set; }
        public int DownVotes { get; private set; }
        public int RepliesCount { get; private set; }
        public ICollection<CommentVote> Votes { get; private set; } = [];

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }


        private ThreadComment(string content, string fanId)
        {
            Content = content;
            FanId = fanId;
            IsDeleted = false;
        }

        public static Result<ThreadComment> Create(string content, string fanId)
        {
            try
            {
                CheckRule(new CommentContentMustBeValid(content));
                CheckRule(new FanIdCannotBeEmpty(fanId));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<ThreadComment>.Failure(e.Details);
            }
            return Result<ThreadComment>.Success(new ThreadComment(content, fanId));
        }

        public void UpdateRepliesCount(int delta)
        {
            RepliesCount += delta;
        }

        public void MarkAsAdded()
        {
            AddDomainEvent(new ThreadCommentsCountUpdatedDomainEvent(+1, FanId, TeamThreadId, GameThreadId));
            if (ParentId.HasValue)
            {
                AddDomainEvent(new ThreadCommentRepliesCountUpdatedDomainEvent(+1, ParentId.Value));
            }
        }

        public void MarkAsDeleted()
        {
            AddDomainEvent(new ThreadCommentsCountUpdatedDomainEvent(-1, FanId, TeamThreadId, GameThreadId));
            if (ParentId.HasValue)
                AddDomainEvent(new ThreadCommentRepliesCountUpdatedDomainEvent(-1, ParentId.Value));
        }

        public void NotifyCommentOwner(string commentOwnerId, Fan fan)
        {
            if (fan.Id != FanId)
                return;

            AddDomainEvent(new ReplyAddedToCommentDomainEvent(
                commentOwnerId,
        fan.Id,
                Config.ReplyAddedNotificationTitle,
                Config.ReplyAddedNotificationTitleContent(fan.Username),
                fan.AvatarPhotoUrl,
                Id.ToString())
            );
        }

        public void NotifyThreadOwner(string threadOwnerId, Fan fan)
        {
            if (fan.Id != FanId)
                return;

            AddDomainEvent(new CommentAddedThreadNotificationDomainEvent(
                threadOwnerId,
                fan.Id,
                Config.CommentAddedThreadNotificationTitle,
                Config.CommentAddedThreadNotificationContent(fan.Username),
                fan.AvatarPhotoUrl,
                Id.ToString())
            );
        }

        public void UpVote()
        {
            UpVotes++;
        }

        public void DownVote()
        {
            DownVotes++;
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
        }

        public void RemoveVote(bool isUpVote)
        {
            if (isUpVote)
                UpVotes--;
            else
                DownVotes--;
        }
        public void Update(string content)
        {
            Content = content;
        }

        public void AttachRespondsToFanId(string fanId)
        {
            RespondsToId = fanId;
        }

        public void AttachParentId(Guid parentId)
        {
            try
            {
                CheckRule(new ParentCommentIdCannotBeEmpty(parentId));
                ParentId = parentId;
            }
            catch (BusinessRuleValidationException) { }
        }

        public void AttachTeamThread(Guid? teamThreadId)
        {
            TeamThreadId = teamThreadId;
        }

        public void AttachGameThread(Guid? gameThreadId)
        {
            GameThreadId = gameThreadId;
        }
    }
}
