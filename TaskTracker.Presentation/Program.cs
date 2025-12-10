//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();




//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
////var builder = WebApplication.CreateBuilder(args);

////// -----------------------------------------
////// Dependency Injection registration
////// -----------------------------------------
////builder.Services.AddApplicationServices();
////builder.Services.AddInfrastructureServices(builder.Configuration);
////builder.Services.AddPersistenceServices(builder.Configuration);

////// -----------------------------------------
////// Add services to the container
////// -----------------------------------------
////builder.Services.AddControllers();
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

////var app = builder.Build();

////// -----------------------------------------
////// Configure middleware
////// -----------------------------------------
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

////app.UseHttpsRedirection();

////app.UseAuthorization();

////app.MapControllers();

////app.Run();
///



using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskTracker.Application.Behaviors;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Application.Mappings;
using TaskTracker.Infrastructure.Options;
using TaskTracker.Infrastructure.Services.Auth;
using TaskTracker.Persistence.Context;
using TaskTracker.Persistence.Repositories;
using TaskTracker.Persistence.UnitOfWork;
using AutoMapper;
using TaskTracker.Presentation.Middleware;

using TaskTracker.Database;
var builder = WebApplication.CreateBuilder(args);


// Configuration

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.Section));

var jwtSettings = builder.Configuration.GetSection(JwtOptions.Section).Get<JwtOptions>();

// Database

builder.Services.AddDbContext<TaskTrackerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("TaskTracker.Persistence")));

builder.Services.AddTransient<DatabaseInitializer>();

// Repositories & Unit of Work

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Services

builder.Services.AddScoped<IAuthService, JwtTokenService>();
builder.Services.AddHttpContextAccessor();


//MediatR

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(TaskTracker.Application.Commands.Boards.CreateBoardCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


// AutoMapper

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

// FluentValidation

builder.Services.AddValidatorsFromAssembly(
    typeof(TaskTracker.Application.Validators.CreateBoardValidator).Assembly);


// Authentication & Authorization

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.Issuer,
        ValidAudience = jwtSettings?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings?.SecretKey ?? throw new InvalidOperationException("JWT SecretKey not configured")))
    };
});

builder.Services.AddAuthorization();


// Controllers & API

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Swagger with JWT Support

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskTracker API",
        Version = "v1",
        Description = "TaskTracker"
    });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    
    initializer.Initialize();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();


// Middleware Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskTracker API V1");
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

