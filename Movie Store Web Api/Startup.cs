using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movie_Store_Web_Api.DBOperations;
using Movie_Store_Web_Api.Middlewares;
using Movie_Store_Web_Api.Services;
using System.Reflection;
using System.Text.Json.Serialization;
using Movie_Store_Web_Api.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Movie_Store_Web_Api.Application.CustomerOperations.Commands.CreateCustomerOperations;
using MediatR;

namespace Movie_Store_Web_Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddDbContext<MovieStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "MovieStoreDb"));

        // If i inject this service it corresponds to BookStoreDbContext
        services.AddScoped<IMovieStoreDbContext>(provider => provider.GetService<MovieStoreDbContext>());

        var JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

        services.AddHttpContextAccessor();

        services.AddAuthorization();

        services.AddSingleton<ILoggerService, DBLogger>();
        services.AddSingleton<ILoggerService, ConsoleLogger>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieStore Api", Version = "v1.0" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "MovieStore Management for IT Company",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
        });

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
                ValidAudience = JwtConfig.Audience,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieStore v1"));
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseCustomExceptionMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
