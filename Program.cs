using GoldenGemsBackEnd.Configurations;
using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Middleware;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Services.Auth.Services;
using GoldenGemsBackEnd.Services.Auth.Interfaces;
using GoldenGemsBackEnd.Repositories.Admin.Interfaces;
using GoldenGemsBackEnd.Repositories.Admin;
using GoldenGemsBackEnd.Services.Admin.Interfaces;
using GoldenGemsBackEnd.Services.Admin.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

// Add services to the container
builder.Services.AddControllers();

// Auth Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Admin Services - Repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IActionRepository, ActionRepository>();
builder.Services.AddScoped<IActionTypeRepository, ActionTypeRepository>();

// Admin Services - Services
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IActionService, ActionService>();

// DbContext (PostgreSQL)
builder.Services.AddDbContext<GoldenGemsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtSettings = jwtSection.Get<JwtSettings>() ?? new JwtSettings();

jwtSettings.SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET") ?? jwtSettings.SecretKey;
jwtSettings.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? jwtSettings.Issuer;
jwtSettings.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? jwtSettings.Audience;

var expirationFromEnv = Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_MINUTES");
if (int.TryParse(expirationFromEnv, out var expirationMinutes))
{
    jwtSettings.AccessTokenExpirationMinutes = expirationMinutes;
}

if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey))
{
    throw new InvalidOperationException("Jwt:SecretKey o JWT_SECRET no están configurados.");
}

builder.Services.Configure<JwtSettings>(options =>
{
    options.Issuer = jwtSettings.Issuer;
    options.Audience = jwtSettings.Audience;
    options.SecretKey = jwtSettings.SecretKey;
    options.AccessTokenExpirationMinutes = jwtSettings.AccessTokenExpirationMinutes;
});

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = signingKey,
        ClockSkew = TimeSpan.Zero
    };
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "GoldenGems API",
        Version = "v1",
        Description = "API Backend para GoldenGems"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GoldenGems API v1");
        c.RoutePrefix = string.Empty; // Swagger UI en la raíz
    });
}

// Middleware personalizado para manejo de excepciones
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// CORS debe ir antes de Authorization
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Initialize default roles
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GoldenGemsDbContext>();

    if (!context.Roles.Any(r => r.Name.ToLower() == "user"))
    {
        context.Roles.Add(new Role
        {
            Id = Guid.NewGuid(),
            Name = "User",
            Description = "Rol por defecto para usuarios registrados",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });
        await context.SaveChangesAsync();
    }
}

app.Run();
