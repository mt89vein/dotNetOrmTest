using System;
using System.Linq;
using Infrastructure.DataProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ObjectFactory = Domain.ObjectFactory;

namespace OrmTest
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
				w => w.UseLazyLoadingProxies(false).UseSqlServer(Configuration.GetConnectionString("Default")), ServiceLifetime.Scoped);
			services.AddMvc()
				.AddJsonOptions(
					options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				);
			RegisterRepositoriesAndServices(ref services);
			services.AddCors();
			ObjectFactory.Instance.Initialize(services.BuildServiceProvider().GetService);
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

			var registrations = repositoryAssembly.GetExportedTypes()
				.Where(t => t.Namespace == "Infrastructure.DataProvider.Repositories" ||
				            t.Namespace == "Integration.Services")
				.SelectMany(t => t.GetInterfaces().Select(i => new {Service = i, Implementation = t}));

			foreach (var reg in registrations) serviceCollection.AddTransient(reg.Service, reg.Implementation);
		}
	}
}