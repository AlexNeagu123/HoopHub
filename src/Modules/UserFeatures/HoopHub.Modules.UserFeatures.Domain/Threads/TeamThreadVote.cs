using HoopHub.Modules.UserFeatures.Domain.Fans;
using System.ComponentModel.DataAnnotations;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using HoopHub.Modules.UserFeatures.Domain.Rules;
using HoopHub.Modules.UserFeatures.Domain.Threads.Events;

namespace HoopHub.Modules.UserFeatures.Domain.Threads
{
    public class TeamThreadVote : AuditableEntity, ISoftDeletable
    {
        public Guid Id { get; private set; }
        public Guid TeamThreadId { get; private set; }

        [Required]
        public TeamThread TeamThread { get; private set; } = null!;
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public bool IsUpVote { get; private set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        private TeamThreadVote(Guid teamThreadId, string fanId, bool isUpVote)
        {
            Id = Guid.NewGuid();
            TeamThreadId = teamThreadId;
            FanId = fanId;
            IsUpVote = isUpVote;
        }

        public void Update(bool isUpVote)
        {
            if (IsUpVote == isUpVote)
                return;

            IsUpVote = isUpVote;
            AddDomainEvent(new TeamThreadVoteUpdatedDomainEvent(TeamThreadId, FanId, IsUpVote));
        }

        public void MarkAsAdded()
        {
            AddDomainEvent(new TeamThreadVoteAddedDomainEvent(TeamThreadId, FanId, IsUpVote));
        }

        public void MarkAsDeleted()
        {
            AddDomainEvent(new TeamThreadVoteDeletedDomainEvent(TeamThreadId, FanId, IsUpVote));
        }

        public static Result<TeamThreadVote> Create(Guid teamThreadId, string fanId, bool isUpVote)
        {
            try
            {
                CheckRule(new ThreadIdCannotBeEmpty(teamThreadId));
                CheckRule(new FanIdCannotBeEmpty(fanId));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<TeamThreadVote>.Failure(e.Details);
            }

            return Result<TeamThreadVote>.Success(new TeamThreadVote(teamThreadId, fanId, isUpVote));
        }
    }
}
