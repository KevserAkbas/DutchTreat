using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();

            //web sunucumuzu bir þeyler yapacak þekilde yapýlandýrmak istediðimizi söylemek istiyoruz
            //her aramada bu cevabý almak yerine, bir þeyler yapmasýný istemek için
            app.UseStaticFiles(); //web sunucusu ortaya çýktýðýnda yapabileciði bir þey olarak statik dosyalar sunma hizmeti eklemesini söyleriz
        }
    }
}
