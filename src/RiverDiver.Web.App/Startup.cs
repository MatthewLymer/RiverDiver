using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Rewrite.Internal;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RiverDiver.Web.App
{
    internal sealed class Startup
    {
        private readonly IConfiguration _configuration;

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(CreateStaticFileOptions());

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseRewriter(CreateAspxRewriteOptions());
        }

        private static StaticFileOptions CreateStaticFileOptions()
        {
            return new StaticFileOptions
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider
                {
                    Mappings =
                    {
                        [".less"] = "text/plain"
                    }
                }
            };
        }

        private static RewriteOptions CreateAspxRewriteOptions()
        {
            return new RewriteOptions
            {
                Rules =
                {
                    new RedirectRule("(.*)\\.[aA][sS][pP][xX]", "$1", (int) HttpStatusCode.PermanentRedirect)
                }
            };
        }
    }
}
