using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace IdentityServeSample.Api
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

            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.Authority = "http://localhost:5000";
                        options.RequireHttpsMetadata = false;
                        options.Audience = "MyApi";
                        // options.Events = new JwtBearerEvents
                        // {
                        //     //AccessToken 验证失败
                        //     OnChallenge = op =>
                        //     {
                        //         //跳过所有默认操作
                        //         op.HandleResponse();
                        //         //下面是自定义返回消息
                        //         //op.Response.Headers.Add("token", "401");
                        //         op.Response.ContentType = "application/json";
                        //         op.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //         op.Response.WriteAsync(JsonConvert.SerializeObject(new
                        //         {
                        //             status = StatusCodes.Status401Unauthorized,
                        //             msg = "token无效"
                        //         }));
                        //         return Task.CompletedTask;
                        //     }
                        // };
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}