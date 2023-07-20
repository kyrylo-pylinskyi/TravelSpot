using Api.Data;
using Api.Models.Entities.Identity;
using Api.Services.Security;
using Api.Services.Smtp;
using Api.Services.TokenProviders;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
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

            services.AddControllers();

            // Google Auth configurations
            var googleAuthConfig = Configuration.GetSection("Authentication:Google");
            services.Configure<GoogleAuthOptions>(googleAuthConfig);

            //Mail Service configurations
            var mailSettings = Configuration.GetSection("MailSettings");
            services.Configure<MailOptions>(mailSettings);
            services.AddTransient<IMailService, MailService>();

            services.AddScoped<IUserTwoFactorTokenProvider<ApplicationUser>, EmailConfirmationTokenProvider<ApplicationUser>>();

            services.AddEndpointsApiExplorer();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "EmailConfirmation";
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Tokens.EmailConfirmationTokenProvider = "EmailConfirmation";
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("EmailConfirmation");

            services.AddCors(options =>
            {
                var client = Configuration.GetValue<string>("TravelSpotApp");

                options.AddPolicy("TravelSpotApp", builder =>
                {
                    builder.WithOrigins(client)
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    //Jwt Auth configurations
                    var jwtAuthConfig = Configuration.GetSection("Authentication:Jwt");
                    services.Configure<JwtAuthOptions>(jwtAuthConfig);
                    var jwtAuthOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtAuthOptions>>().Value;


                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtAuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtAuthOptions.Audience,
                        ValidateLifetime = true,
                        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                                            notBefore <= DateTime.UtcNow && expires > DateTime.UtcNow,
                        IssuerSigningKey = jwtAuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                })
                .AddCookie()
                .AddGoogle(options =>
                {
                    // Google Auth configurations
                    var googleAuthOptions = services.BuildServiceProvider().GetRequiredService<IOptions<GoogleAuthOptions>>().Value;

                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClientId = googleAuthOptions.ClientId;
                    options.ClientSecret = googleAuthOptions.ClientSecret;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var googleAuthOptions = app.ApplicationServices.GetRequiredService<IOptions<GoogleOptions>>().Value;

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.html", "TravelSpot API");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("TravelSpotApp");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
