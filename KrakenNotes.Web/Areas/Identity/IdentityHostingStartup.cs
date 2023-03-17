using System;
using KrakenNotes.Data.Models;
using KrakenNotes.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(KrakenNotes.Web.Areas.Identity.IdentityHostingStartup))]
namespace KrakenNotes.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<KrakenNotesContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("KrakenNotesContextConnection")));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<KrakenNotesContext>();

                services.Configure<IdentityOptions>(options => 
                {
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                });
            });
        }
    }
}