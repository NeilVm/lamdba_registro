using Microsoft.AspNetCore.Builder;
using registro.Data;
using MySql.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using registro.Services;


namespace registro;

public class Startup
{
    public Startup(IConfiguration configuration) 
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddScoped<AuthService>();
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
        services.AddSingleton<BCrypt.Net.BCrypt>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerUI();
            app.UseSwagger();
           

        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseCors("AllowAllOrigins");

        app.UseAuthorization();


        IApplicationBuilder applicationBuilder = app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}