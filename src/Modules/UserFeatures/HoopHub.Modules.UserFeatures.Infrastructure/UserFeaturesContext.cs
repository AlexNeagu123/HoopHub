using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure
{
    public class UserFeaturesContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public DbSet<Fan> Fans { get; set; }
        public DbSet<TeamThread> TeamThreads { get; set; }
        public DbSet<GameThread> GameThreads { get; set; }
        public DbSet<ThreadComment> Comments { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }

        public UserFeaturesContext(DbContextOptions<UserFeaturesContext> options, ICurrentUserService userService) :
            base(options)
        {
            _currentUserService = userService;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _currentUserService.GetCurrentClaimsPrincipal().Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State is EntityState.Added or EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = _currentUserService.GetCurrentClaimsPrincipal().Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("user_features");
            ModelFansTable(modelBuilder);
            ModelTeamThreadsTable(modelBuilder);
            ModelGameThreadsTable(modelBuilder);
            ModelCommentsTable(modelBuilder);
            ModelCommentVotesTable(modelBuilder);
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
