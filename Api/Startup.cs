using Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
			services.AddMvc();

			services.AddSingleton(new Repository(Configuration["Token"], Configuration["DBName"]));
			services.AddSingleton(Configuration);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseMvc();
		}
	}
}