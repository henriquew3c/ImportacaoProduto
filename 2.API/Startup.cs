using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using _2.API.Infra;
using _2.API.Repository;
using _Support;
using MediatR;

namespace _2.API
{
    public class Startup
    {
        public ApplicationContextService ApplicationContextService { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ApplicationContextService = new ApplicationContextService(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "_2.API", Version = "v1" });
            });

            ApplicationContextService.Configure(services);

            services.AddSingleton<IValidationState, MvcValidator>();
            services.AddSingleton<IDomainValidation, BasicValidation>();
            services.AddSingleton<IUnityOfWork, UnityOfWork>();
            services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();
            services.AddSingleton<IImportacaoRepository, ImportacaoRepository>();

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "_2.API v1"));
            }

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
