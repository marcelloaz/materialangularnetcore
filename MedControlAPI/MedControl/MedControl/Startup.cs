using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedControl.Entities;
using MedControl.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;

namespace MedControl
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
          .AddJsonOptions(options =>
          {
              options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
              options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                  //new CamelCasePropertyNamesContractResolver();
          });

            //cross
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOriginsHeadersAndMethods",
                    builder => builder.WithOrigins("http://localhost:4200/").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            var connectionString = Startup.Configuration["connectionStrings:MedControlDB"];
            services.AddDbContext<MedControlsContext>(o => o.UseSqlServer(connectionString));

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ICompromissoRepository, CompromissoRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            MedControlsContext medControlsContext)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            medControlsContext.SemeiaDadosParaDBContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Compromisso, Dtos.Compromisso>();
                cfg.CreateMap<Entities.Consulta, Dtos.Consulta>();
                cfg.CreateMap<Entities.Paciente, Dtos.Paciente>();
            });

            app.UseCors("AllowAllOriginsHeadersAndMethods");
            app.UseMvc();

        }
    }
}
