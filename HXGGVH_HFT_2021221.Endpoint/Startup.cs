using HXGGVH_HFT_2021221.Data;
using HXGGVH_HFT_2021221.Endpoint.Services;
using HXGGVH_HFT_2021221.Logic;
using HXGGVH_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IPokemonLogic, PokemonLogic>();
            services.AddSingleton<ITrainerLogic, TrainerLogic>();
            services.AddSingleton<IRegionLogic, RegionLogic>();
            services.AddSingleton<IPokemonRepository, PokemonRepository>();
            services.AddSingleton<ITrainerRepository, TrainerRepository>();
            services.AddSingleton<IRegionRepository, RegionRepository>();
            services.AddSingleton<TrainerDbContext, TrainerDbContext>();

            services.AddSignalR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title ="HXGGVH_HFT_2021221.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HXGGVH_HFT_2021221.Endpoint v1"));
            }

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:44531")); //44531 , 35206

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });

        }
    }
}
