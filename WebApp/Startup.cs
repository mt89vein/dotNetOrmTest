using System;
using System.Linq;
using Infrastructure.DataProvider;
using Infrastructure.DataProvider.Caching;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(
                w => w.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );
            RegisterRepositoriesAndServices(ref services);
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes => routes.MapRoute(
                "api-default",
                "api/{controller=Home}/{action=Index}/{id?}"));
        }

        private static void RegisterRepositoriesAndServices(ref IServiceCollection serviceCollection)
        {
            var repositoryAssembly = typeof(ApplicationContext).Assembly;

            foreach (var type in repositoryAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.Namespace == "Infrastructure.DataProvider.Repositories" ||
                            t.Namespace == "Infrastructure.DataProvider.Services" ||
                            t.Namespace == "Integration.Services"))
            {
                foreach (var i in type.GetInterfaces())
                {
                    serviceCollection.AddScoped(
                        i.IsGenericType
                            ? i.GetGenericTypeDefinition().MakeGenericType(i.GetGenericArguments())
                            : i,
                        type);
                }
            }

            serviceCollection.AddScoped(typeof(IRedisService <,>), typeof(RedisService<,>));
        }
    }
}