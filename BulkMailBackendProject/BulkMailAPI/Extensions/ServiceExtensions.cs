using System.Text;
using BulkMailAPI.Middlewares;
using DataAccessAndEntities.Entities;
using DTOs.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services;
using ServicesInterfaces;

namespace BulkMailAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            string[] allowedOrigins = configuration.GetSection("AllowedOrigins")?.Value?.Split(",") ?? new string[0];
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDB"));
            services.Configure<JwtOptions>(configuration.GetSection("jwt"));
            services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
            
            var serviceProvider = services.BuildServiceProvider();
            IOptions<JwtOptions> jwtOptions = (IOptions<JwtOptions>)serviceProvider.GetService(typeof(IOptions<JwtOptions>));
            var jwtOptionsValues = jwtOptions.Value;
            
            var mongoSettingsOption = (IOptions<MongoDBSettings>)serviceProvider.GetService(typeof(IOptions<MongoDBSettings>));
            var mongoDbSettings = mongoSettingsOption.Value;
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: "myAllowedOriginsPolicy",
                                  policy =>
                                  {
                                      policy.WithOrigins(allowedOrigins).AllowAnyHeader()
                                                              .AllowAnyMethod().AllowCredentials();
                                  });
            });
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.ASCII.GetBytes(jwtOptionsValues?.JwtKey);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false, // for dev
                        ValidateAudience = false, // for dev
                        RequireExpirationTime = false, // for dev -- needs to be updated when refresh token is added
                        ValidateLifetime = true
                    };
                });

            services.AddAuthorization();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<SessionInfo>();
            
            //Middleare services
            services.AddTransient<TokenMiddleware>();
            
            services.AddTransient<CancellationTokenSource>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddControllers();

            services.AddDbContext<BulkMailDbContext>(options =>
            {
                options.UseMongoDB(mongoDbSettings.ConnectionURI, mongoDbSettings.DatabaseName);
            });

            return services;
        }
    }
}
