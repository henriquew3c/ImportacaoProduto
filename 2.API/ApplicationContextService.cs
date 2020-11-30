using _2.API.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _2.API
{
    public class ApplicationContextService
    {
        private IConfiguration Configuration { get; }

        public ApplicationContextService(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
        }
    }
}