using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(_5.Areas.Identity.IdentityHostingStartup))]
namespace _5.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}