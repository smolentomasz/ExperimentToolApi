using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ExperimentToolApi
{
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
            services.AddSwaggerGen(c => c.SwaggerDoc("1.0", new OpenApiInfo{ Title = "Experiment Tool API", Version = "1.0" }));
            services.AddDbContext<ExperimentToolDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("ExperimentToolConnection")));
            services.AddTransient<ICompressionTestRepository, CompressionTestRepository>();
            services.AddTransient<ICompressionResultRepository, CompressionResultRepository>();
            services.AddTransient<ITensileTestRepository, TensileTestRepository>();
            services.AddTransient<ITensileResultRepository, TensileResultRepository>();
            services.AddTransient<IMaterialRepository, MaterialRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>{
                c.SwaggerEndpoint("/swagger/1.0/swagger.json","Experiment Tool API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
