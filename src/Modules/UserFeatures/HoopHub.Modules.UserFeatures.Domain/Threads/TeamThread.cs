using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Threads
{
    public class TeamThread : BaseThread, ISoftDeletable
    {
        public Guid TeamId { get; private set; }
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int UpVotes { get; private set; }
        public int DownVotes { get; private set; }
        public ICollection<TeamThreadVote> Votes { get; private set; } = [];
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        private TeamThread(string fanId, Guid teamId, string title, string content)
        {
            FanId = fanId;
            TeamId = teamId;
            Title = title;
            Content = content;
        }

        public static Result<TeamThread> Create(string fanId, Guid teamId, string title, string content)
        {
            try
            {
                CheckRule(new FanIdCannotBeEmpty(fanId));
                CheckRule(new BothTeamIdsAreRequired(teamId));
                CheckRule(new TitleMustBeValid(title));
                CheckRule(new ThreadContentMustBeValid(content));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<TeamThread>.Failure(e.Details);
            }
            return Result<TeamThread>.Success(new TeamThread(fanId, teamId, title, content));
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

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
