using System.Text;
using HoopHub.API.Extensions;
using HoopHub.API.Utility;
using HoopHub.BuildingBlocks.Application.Persistence;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserAccessContext>()
    .AddDefaultTokenProviders();


// UserAccess STUFF
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserAccessContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
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
builder.Services.AddScoped<ISeasonAverageStatsService, SeasonAverageStatsService>();
builder.Services.AddScoped<IGamesDataService, GamesDataService>();
builder.Services.AddScoped<IBoxScoresDataService, BoxScoresDataService>();

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

app.UseCors("CorsPolicy");

app.Run();