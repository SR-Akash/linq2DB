using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BMS.DBMigration
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services.AddDbContext<ApplicationContext>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
