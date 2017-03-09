using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.FileProviders;

namespace goldencardAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var connection = Configuration.GetConnectionString("MS_TableConnectionString");

            //var connection = @"Server=.;Database=Heroes;Trusted_Connection=True;";


            //services.AddDbContext<Models.HeroContext>(options => options.UseInMemoryDatabase());
            //services.AddDbContext<Models.HeroContext>(options => options.UseSqlServer(connection));
            //services.AddTransient(typeof(Repositories.HeroRepository));
            
            // Add framework services.
            services.AddMvc();
            services.AddSignalR();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            //services.AddSwaggerGen();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                //app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });
            
            app.UseMvc();
            app.UseWebSockets();
            app.UseSignalR();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            //app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            //app.UseSwaggerUi();
        }
    }
}
