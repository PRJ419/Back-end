using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Areas.Identity.Data;
using WebApi.Models;

[assembly: HostingStartup(typeof(WebApi.Areas.Identity.IdentityHostingStartup))]
namespace WebApi.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BarOMeterIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BarOMeterIdentityContextConnection")));

                services.AddDefaultIdentity<BarOMeterIdentityUser>()
                    .AddEntityFrameworkStores<BarOMeterIdentityContext>();
            });
        }
    }
}