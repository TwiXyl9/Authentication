using System;
using lab5.Areas.Identity.Data;
using lab5.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(lab5.Areas.Identity.IdentityHostingStartup))]
namespace lab5.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<lab5Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("lab5ContextConnection")));

                services.AddDefaultIdentity<lab5User>(options => {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequiredLength = 1;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<lab5Context>();
            });
        }
    }
}