using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using HoopHub.Modules.UserFeatures.Domain.FanNotifications;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Follows;
using HoopHub.Modules.UserFeatures.Domain.Reviews;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure
{
    public class UserFeaturesContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPublisher _publisher;
        public DbSet<Fan> Fans { get; set; }
        public DbSet<TeamThread> TeamThreads { get; set; }
        public DbSet<GameThread> GameThreads { get; set; }
        public DbSet<ThreadComment> Comments { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }
        public DbSet<GameReview> GameReviews { get; set; }
        public DbSet<PlayerPerformanceReview> PlayerPerformanceReviews { get; set; }
        public DbSet<TeamFollowEntry> TeamFollowEntries { get; set; }
        public DbSet<PlayerFollowEntry> PlayerFollowEntries { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public UserFeaturesContext(DbContextOptions<UserFeaturesContext> options, ICurrentUserService userService, IPublisher publisher) :
            base(options)
        {
            _currentUserService = userService;
            _publisher = publisher;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State is EntityState.Added or EntityState.Modified)
                {
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }


            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("user_features");
            ModelFansTable(modelBuilder);
            ModelTeamThreadsTable(modelBuilder);
            ModelGameThreadsTable(modelBuilder);
            ModelCommentsTable(modelBuilder);
            ModelCommentVotesTable(modelBuilder);
            ModelGameReviewsTable(modelBuilder);
            ModelPlayerPerformanceReviewsTable(modelBuilder);
            ModelTeamFollowEntriesTable(modelBuilder);
            ModelPlayerFollowEntriesTable(modelBuilder);
            ModelNotificationsTable(modelBuilder);
        }

        private static void ModelTeamFollowEntriesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamFollowEntry>().HasKey(tfe => new { tfe.FanId, tfe.TeamId });
            modelBuilder.Entity<TeamFollowEntry>().HasOne(tfe => tfe.Fan).WithMany(f => f.TeamFollowEntries).HasForeignKey(tfe => tfe.FanId);
            modelBuilder.Entity<TeamFollowEntry>().ToTable("team_follow_entries");
        }

        private static void ModelPlayerFollowEntriesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerFollowEntry>().HasKey(pfe => new { pfe.FanId, pfe.PlayerId });
            modelBuilder.Entity<PlayerFollowEntry>().HasOne(pfe => pfe.Fan).WithMany(f => f.PlayerFollowEntries).HasForeignKey(pfe => pfe.FanId);
            modelBuilder.Entity<PlayerFollowEntry>().ToTable("player_follow_entries");
        }

        private static void ModelNotificationsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>().HasKey(n => n.Id);
            modelBuilder.Entity<Notification>().HasOne(n => n.Recipient).WithMany(f => f.NotificationsReceived).HasForeignKey(n => n.RecipientId);
            modelBuilder.Entity<Notification>().HasOne(n => n.Sender).WithMany(f => f.NotificationsSent).HasForeignKey(n => n.SenderId);
            modelBuilder.Entity<Notification>().ToTable("notifications");
        }

        private static void ModelGameReviewsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameReview>().HasKey(gr => new { gr.HomeTeamId, gr.VisitorTeamId, gr.Date });
            modelBuilder.Entity<GameReview>().HasOne(gr => gr.Fan).WithMany(f => f.GameReviews).HasForeignKey(gr => gr.FanId);
            modelBuilder.Entity<GameReview>().ToTable("game_reviews");
        }

        private static void ModelPlayerPerformanceReviewsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerPerformanceReview>().HasKey(ppr => new { ppr.HomeTeamId, ppr.VisitorTeamId, ppr.Date });
            modelBuilder.Entity<PlayerPerformanceReview>().HasOne(ppr => ppr.Fan).WithMany(f => f.PlayerPerformanceReviews).HasForeignKey(ppr => ppr.FanId);
            modelBuilder.Entity<PlayerPerformanceReview>().ToTable("player_performance_reviews");
        }

        private static void ModelFansTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fan>().HasKey(f => f.Id);
            modelBuilder.Entity<Fan>().HasMany(f => f.TeamThreads).WithOne(t => t.Fan).HasForeignKey(tt => tt.FanId);
            modelBuilder.Entity<Fan>().HasMany(f => f.Comments).WithOne(t => t.Fan).HasForeignKey(c => c.FanId);
            modelBuilder.Entity<Fan>().HasMany(f => f.Votes).WithOne(t => t.Fan).HasForeignKey(v => v.FanId);
            modelBuilder.Entity<Fan>().ToTable("fans");
        }

        private static void ModelTeamThreadsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamThread>().HasKey(tt => tt.Id);
            modelBuilder.Entity<TeamThread>().HasOne(tt => tt.Fan).WithMany(f => f.TeamThreads).HasForeignKey(tt => tt.FanId);
            modelBuilder.Entity<TeamThread>().HasMany(tt => tt.Comments).WithOne(c => c.TeamThread).HasForeignKey(c => c.TeamThreadId);
            modelBuilder.Entity<TeamThread>().HasQueryFilter(t => !t.IsDeleted);

            modelBuilder.Entity<TeamThread>().ToTable("team_threads");
        }

        private static void ModelGameThreadsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameThread>().HasKey(gt => gt.Id);
            modelBuilder.Entity<GameThread>().HasMany(gt => gt.Comments).WithOne(c => c.GameThread).HasForeignKey(c => c.GameThreadId);
            modelBuilder.Entity<GameThread>().ToTable("game_threads");
        }

        private static void ModelCommentsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThreadComment>().HasKey(c => c.Id);
            modelBuilder.Entity<ThreadComment>().HasOne(c => c.TeamThread).WithMany(tt => tt.Comments).HasForeignKey(c => c.TeamThreadId);
            modelBuilder.Entity<ThreadComment>().HasOne(c => c.GameThread).WithMany(gt => gt.Comments).HasForeignKey(c => c.GameThreadId);
            modelBuilder.Entity<ThreadComment>().HasOne(c => c.Fan).WithMany(f => f.Comments).HasForeignKey(c => c.FanId);
            modelBuilder.Entity<ThreadComment>().HasMany(c => c.Votes).WithOne(v => v.ThreadComment).HasForeignKey(v => v.CommentId);
            modelBuilder.Entity<ThreadComment>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<ThreadComment>().ToTable("comments");
        }

        private static void ModelCommentVotesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentVote>().HasKey(v => new { v.CommentId, v.FanId });
            modelBuilder.Entity<CommentVote>().HasOne(v => v.ThreadComment).WithMany(tc => tc.Votes).HasForeignKey(v => v.CommentId);
            modelBuilder.Entity<CommentVote>().HasOne(v => v.Fan).WithMany(f => f.Votes).HasForeignKey(v => v.FanId);
            modelBuilder.Entity<CommentVote>().ToTable("comment_votes");
        }
    }
}
