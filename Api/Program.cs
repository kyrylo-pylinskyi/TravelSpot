using Api.Data;
using Api.Models.Entities;
using Api.Models.Entities.Identity;
using Api.Services.Security;
using Api.Services.Smtp;
using Api.Services.TokenProviders;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Configuration;
using static Org.BouncyCastle.Math.EC.ECCurve;

//Initialize builder
var builder = WebApplication.CreateBuilder(args);

//Web application provider and configuration
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();


//Web application db connection string
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();

//Mail Service configurations
var mailSettings = builder.Configuration.GetSection("MailSettings");

// Add the configuration for MailSettings
builder.Services.Configure<MailSettings>(mailSettings);

builder.Services.AddTransient<IMailService, MailService>();

// Add Two factor email token provider
builder.Services.AddScoped<IUserTwoFactorTokenProvider<ApplicationUser>, EmailConfirmationTokenProvider<ApplicationUser>>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation");


builder.Services.AddCors(options =>
{
    var client = configuration.GetValue<string>("TravelSpotApp");

    options.AddPolicy("TravelSpotApp", builder =>
    {
        builder.WithOrigins(client)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//Jwt settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<AuthOptions>(jwtSettings);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var authOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<AuthOptions>>().Value;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = authOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    })
    .AddGoogle(options =>
    {
        IConfigurationSection googleAuthNSection =
        configuration.GetSection("Authentication:Google");
        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("TravelSpotApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
