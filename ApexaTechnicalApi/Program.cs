using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using ApexaTechnicalApi.MappingProfiles;
using ApexaTechnicalApi.Data;
using ApexaTechnicalApi.Services;
using ApexaTechnicalApi.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Setting up  JWT in a dedicated service
builder.Services.ConfigureJwt(builder.Configuration);


// Adding services to the container
builder.Services.AddControllers();

// Configuring the in-memory database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("AdvisorManagementDb"));

// Configuring AutoMapper with multiples profiles
builder.Services.AddAutoMapper(typeof(AdvisorMappingProfile), typeof(UserMappingProfile));


//Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// Registering custom services
builder.Services.AddScoped<ApexaTechnicalApi.Services.AdvisorService>();

// Registering of AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configuring the HTTP pipelines
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
