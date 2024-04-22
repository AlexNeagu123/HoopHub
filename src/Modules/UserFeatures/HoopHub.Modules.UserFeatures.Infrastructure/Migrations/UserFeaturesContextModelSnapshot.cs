﻿// <auto-generated />
using System;
using HoopHub.Modules.UserFeatures.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    [DbContext(typeof(UserFeaturesContext))]
    partial class UserFeaturesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("user_features")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HoopHub.BuildingBlocks.Domain.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<DateTime>("OccuredOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Comments.CommentVote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUpVote")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("FanId");

                    b.ToTable("comment_votes", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Comments.ThreadComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DownVotes")
                        .HasColumnType("integer");

                    b.Property<string>("FanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("GameThreadId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TeamThreadId")
                        .HasColumnType("uuid");

                    b.Property<int>("UpVotes")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FanId");

                    b.HasIndex("GameThreadId");

                    b.HasIndex("TeamThreadId");

                    b.ToTable("comments", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.FanNotifications.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AttachedImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("AttachedNavigationData")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RecipientId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderId")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("notifications", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AvatarPhotoUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DownVotes")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FanBadge")
                        .HasColumnType("integer");

                    b.Property<Guid?>("FavouriteTeamId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UpVotes")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("fans", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Follows.PlayerFollowEntry", b =>
                {
                    b.Property<string>("FanId")
                        .HasColumnType("text");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("FanId", "PlayerId");

                    b.ToTable("player_follow_entries", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Follows.TeamFollowEntry", b =>
                {
                    b.Property<string>("FanId")
                        .HasColumnType("text");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("FanId", "TeamId");

                    b.ToTable("team_follow_entries", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Reviews.GameReview", b =>
                {
                    b.Property<Guid>("HomeTeamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VisitorTeamId")
                        .HasColumnType("uuid");

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<string>("FanId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric");

                    b.HasKey("HomeTeamId", "VisitorTeamId", "Date", "FanId");

                    b.HasIndex("FanId");

                    b.ToTable("game_reviews", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Reviews.PlayerPerformanceReview", b =>
                {
                    b.Property<Guid>("HomeTeamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VisitorTeamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<string>("FanId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric");

                    b.HasKey("HomeTeamId", "VisitorTeamId", "PlayerId", "Date", "FanId");

                    b.HasIndex("FanId");

                    b.ToTable("player_performance_reviews", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Threads.GameThread", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("HomeTeamId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("VisitorTeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("game_threads", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Threads.TeamThread", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FanId");

                    b.ToTable("team_threads", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Comments.CommentVote", b =>
                {
                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Comments.ThreadComment", "ThreadComment")
                        .WithMany("Votes")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", "Fan")
                        .WithMany("Votes")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fan");

                    b.Navigation("ThreadComment");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Comments.ThreadComment", b =>
                {
                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", "Fan")
                        .WithMany("Comments")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Threads.GameThread", "GameThread")
                        .WithMany("Comments")
                        .HasForeignKey("GameThreadId");

                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Threads.TeamThread", "TeamThread")
                        .WithMany("Comments")
                        .HasForeignKey("TeamThreadId");

                    b.Navigation("Fan");

                    b.Navigation("GameThread");

                    b.Navigation("TeamThread");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.FanNotifications.Notification", b =>
                {
                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", "Recipient")
                        .WithMany("NotificationsReceived")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", "Sender")
                        .WithMany("NotificationsSent")
                        .HasForeignKey("SenderId");

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Follows.PlayerFollowEntry", b =>
                {
                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", "Fan")
                        .WithMany("PlayerFollowEntries")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fan");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Follows.TeamFollowEntry", b =>
                {
                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", "Fan")
                        .WithMany("TeamFollowEntries")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fan");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Reviews.GameReview", b =>
                {
                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", "Fan")
                        .WithMany("GameReviews")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fan");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Reviews.PlayerPerformanceReview", b =>
                {
                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", "Fan")
                        .WithMany("PlayerPerformanceReviews")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fan");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Threads.TeamThread", b =>
                {
                    b.HasOne("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", "Fan")
                        .WithMany("TeamThreads")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fan");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Comments.ThreadComment", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("GameReviews");

                    b.Navigation("NotificationsReceived");

                    b.Navigation("NotificationsSent");

                    b.Navigation("PlayerFollowEntries");

                    b.Navigation("PlayerPerformanceReviews");

                    b.Navigation("TeamFollowEntries");

                    b.Navigation("TeamThreads");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Threads.GameThread", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Threads.TeamThread", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
