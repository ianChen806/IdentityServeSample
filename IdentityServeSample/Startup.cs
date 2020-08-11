using IdentityServeSample.Config;
using IdentityServeSample.IdentityServer;
using IdentityServeSample.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityServeSample
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
            services.AddControllers()
                    .AddNewtonsoftJson();

            services.AddHttpContextAccessor();

            services.AddIdentityServer()
                    .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                    .AddInMemoryClients(IdentityConfig.GetClients())
                    .AddInMemoryApiScopes(IdentityConfig.GetScopes())
                    .AddDeveloperSigningCredential(false)
                    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                    .AddProfileService<CustomProfileService>();

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            services.AddScoped<ITokenProvider, TokenProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // // one time
            // new DatabaseInit().InitializeDatabase(app);

            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}