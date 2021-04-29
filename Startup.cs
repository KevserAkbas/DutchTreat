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
            //AddControllersWithViews--> genellikle API senaryolarý için
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { //Geliþtirme (Development) aþamasýnda göstermek için / Son kullanýcý görmez
            app.UseDeveloperExceptionPage();//exception larý sayfada göstermek için / developer için
            }

            //web sunucumuzu bir þeyler yapacak þekilde yapýlandýrmak istediðimizi söylemek istiyoruz
            //her aramada bu cevabý almak yerine, bir þeyler yapmasýný istemek için
            app.UseStaticFiles(); //web sunucusu ortaya çýktýðýnda yapabileciði bir þey olarak statik dosyalar sunma hizmeti eklemesini söyleriz

            app.UseRouting();//sunucuya gelen tek tek aramalrý belirli kod parçalarýna yönlendirmeyi saðlar. 

            //Endpoints --> sunucuya gelen istekleri karþýlamaya çalýþmak için iþlenecek bir ara yazýlým kümesi belirlememize olanak tanýr.
            app.UseEndpoints(cfg =>
            {
                //MapControllerRoute --> bir model oluþturmamýza izin verecektir
                cfg.MapControllerRoute("Default", //bu yüzden varsayýlan (default) yol olarak adlandýrýldý.
                    "/{controller}/{action}/{id?}", //sonra aranan / id? --> isteðe baðlý (?, istaðe baðlý olduðunu gösterir - null yapýlabilir) 
                    new { controller = "App", action = "Index" });// anoni nesnede, varsayýlanlarýn ne olduðu
                                                                  //Nedeni, belirtilmemiþse varsayýlanlarý vermek için.
                                                                  //Birisi controller eylem(action) ve kimlik(id) belirtilmemiþ sayfanýn köküne gittiðinde,
                                                                  //varsayýlan olarak AppController dizinine gitmesi söyleniyor. 
            });
        }
    }
}
