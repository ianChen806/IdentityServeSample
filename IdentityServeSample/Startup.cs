using System.Reflection;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("Identity");
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql =>
                        {
                            sql.MigrationsAssembly(assemblyName);
                        });
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql =>
                        {
                            sql.MigrationsAssembly(assemblyName);
                        });
                    })
                    .AddDeveloperSigningCredential();

            // .AddInMemoryApiResources(IdentityConfig.GetApiResources())
            // .AddInMemoryApiScopes(IdentityConfig.GetScopes())
            // .AddInMemoryClients(IdentityConfig.GetClients())
            // .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
            // .AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // one time
            new DatabaseInit().InitializeDatabase(app);

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