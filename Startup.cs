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

            //web sunucumuzu bir �eyler yapacak �ekilde yap�land�rmak istedi�imizi s�ylemek istiyoruz
            //her aramada bu cevab� almak yerine, bir �eyler yapmas�n� istemek i�in
            app.UseStaticFiles(); //web sunucusu ortaya ��kt���nda yapabileci�i bir �ey olarak statik dosyalar sunma hizmeti eklemesini s�yleriz
        }
    }
}
