﻿// <auto-generated />
using System;
using HoopHub.Modules.UserFeatures.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    [DbContext(typeof(UserFeaturesContext))]
    [Migration("20240418204750_OneMoreMore")]
    partial class OneMoreMore
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("user_features")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Comments.CommentVote", b =>
                {
                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.Property<string>("FanId")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsUpVote")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CommentId", "FanId");

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

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DownVotes")
                        .HasColumnType("integer");

                    b.Property<string>("FanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("GameThreadId")
                        .HasColumnType("uuid");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

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

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Fans.Fan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AvatarPhotoUrl")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("fans", "user_features");
                });

            modelBuilder.Entity("HoopHub.Modules.UserFeatures.Domain.Threads.GameThread", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("HomeTeamId")
                        .HasColumnType("uuid");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

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

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

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
