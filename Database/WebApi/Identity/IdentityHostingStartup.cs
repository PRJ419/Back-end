using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Identity;
using WebApi.Identity.Data;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace WebApi.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //var connection = @"Data Source=DESKTOP-UGIDUH3;Initial Catalog=PRJ4Identity;Integrated Security=True";

            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<BarOMeterIdentityContext>(options =>
                    options.UseSqlServer(//connection));
                        context.Configuration.GetConnectionString("BarOMeterIdentityContextConnection")));

                services.AddDefaultIdentity<BarOMeterIdentityUser>()
                    .AddEntityFrameworkStores<BarOMeterIdentityContext>();
            });
        }
    }
}