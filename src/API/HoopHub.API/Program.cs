using System.Text;
using HoopHub.API.BackgroundJobs;
using HoopHub.API.Extensions;
using HoopHub.API.Hubs;
using HoopHub.API.Services;
using HoopHub.API.Utility;
using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.GamesData;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.SeasonAverageStats;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Infrastructure;
using HoopHub.Modules.NBAData.Infrastructure.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Infrastructure.ExternalApiServices.GamesData;
using HoopHub.Modules.NBAData.Infrastructure.ExternalApiServices.SeasonAverageStats;
using HoopHub.Modules.NBAData.Infrastructure.Persistence;
using HoopHub.Modules.UserAccess.Application.Services.Login;
using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Domain.Users;
using HoopHub.Modules.UserAccess.Infrastructure;
using HoopHub.Modules.UserAccess.Infrastructure.Services.Login;
using HoopHub.Modules.UserAccess.Infrastructure.Services.Registration;
using HoopHub.Modules.UserFeatures.Application.Events;
using HoopHub.Modules.UserFeatures.Application.ExternalServices.AzureBlobStorage;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Infrastructure;
using HoopHub.Modules.UserFeatures.Infrastructure.BackgroundJobs;
using HoopHub.Modules.UserFeatures.Infrastructure.ExternalServices.AzureBlobStorage;
using HoopHub.Modules.UserFeatures.Infrastructure.Interceptors;
using HoopHub.Modules.UserFeatures.Infrastructure.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HoopHub API"
    });

    c.OperationFilter<FileResultContentTypeOperationFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("HoopHubConnection");

// NBAData STUFF
builder.Services.AddDbContext<NBADataContext>(options =>
       options.UseNpgsql(
           connectionString,
           o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "nba_data")));

builder.Services.AddDbContext<UserAccessContext>(options =>
    options.UseNpgsql(connectionString,
    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "user_access")));

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IPlayerTeamSeasonRepository, PlayerTeamSeasonRepository>();
builder.Services.AddScoped<ITeamBioRepository, TeamBioRepository>();
builder.Services.AddScoped<ISeasonAverageStatsService, SeasonAverageStatsService>();
builder.Services.AddScoped<IGamesDataService, GamesDataService>();
builder.Services.AddScoped<IBoxScoresDataService, BoxScoresDataService>();
builder.Services.AddScoped<IStandingsRepository, StandingsRepository>();
builder.Services.AddScoped<IPlayoffSeriesRepository, PlayoffSeriesRepository>();
builder.Services.AddScoped<ITeamLatestRepository, TeamLatestRepository>();

// UserAccess STUFF
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserAccessContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserAccessContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"] ?? string.Empty))
    };
});

builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();



// UserFeatures STUFF
builder.Services.AddSingleton<SoftDeleteInterceptor>();
builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

builder.Services.AddDbContext<UserFeaturesContext>((sp, options) =>
    options.UseNpgsql(
        connectionString,
        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "user_features"))
    .AddInterceptors(sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>(), sp.GetRequiredService<SoftDeleteInterceptor>()));


builder.Services.AddScoped<IFanRepository, FanRepository>();
builder.Services.AddScoped<ITeamThreadRepository, TeamThreadRepository>();
builder.Services.AddScoped<IThreadCommentRepository, ThreadCommentRepository>();
builder.Services.AddScoped<IThreadCommentVoteRepository, ThreadCommentVoteRepository>();
builder.Services.AddScoped<IGameReviewRepository, GameReviewRepository>();
builder.Services.AddScoped<IGameThreadRepository, GameThreadRepository>();
builder.Services.AddScoped<IPlayerPerformanceReviewRepository, PlayerPerformanceReviewRepository>();
builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
builder.Services.AddScoped<IPlayerFollowEntryRepository, PlayerFollowEntryRepository>();
builder.Services.AddScoped<ITeamFollowEntryRepository, TeamFollowEntryRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", b =>
{
    b.WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader();
}));


builder.Services.AddSignalR(options =>
{
    options.DisableImplicitFromServicesParameters = true;
});

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<UserRegisteredIntegrationEventHandler>();
    busConfigurator.UsingInMemory((context, config) =>
    {
        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddQuartz(configure =>
{
    var outboxJobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
    var liveBoxScoresJobKey = new JobKey(nameof(LiveBoxScoreJob));

    configure.AddJob<ProcessOutboxMessagesJob>(outboxJobKey)
        .AddTrigger(trigger => trigger
            .ForJob(outboxJobKey)
            .WithSimpleSchedule(schedule => schedule
                .WithIntervalInSeconds(int.Parse(builder.Configuration["BackgroundJobTimes:OutBoxProcessor"] ?? "5"))
                .RepeatForever()));

    configure.AddJob<LiveBoxScoreJob>(liveBoxScoresJobKey)
        .AddTrigger(trigger => trigger
            .ForJob(liveBoxScoresJobKey)
            .WithSimpleSchedule(schedule => schedule
                .WithIntervalInSeconds(int.Parse(builder.Configuration["BackgroundJobTimes:LiveBoxScores"] ?? "10"))
                .RepeatForever()));
});

builder.Services.AddQuartzHostedService();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<LiveBoxScoreHub>("box-scores-live");

app.UseCors("CorsPolicy");

app.Run();
