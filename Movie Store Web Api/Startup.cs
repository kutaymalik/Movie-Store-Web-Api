using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movie_Store_Web_Api.DBOperations;
using System.Reflection;

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
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore", Version = "v1" });
        });

        services.AddDbContext<MovieStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "MovieStoreDb"));

        // If i inject this service it corresponds to BookStoreDbContext
        services.AddScoped<IMovieStoreDbContext>(provider => provider.GetService<MovieStoreDbContext>());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //services.AddSingleton<ILoggerService, DBLogger>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        //app.UseCustomExceptionMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
