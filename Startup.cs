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
            //AddControllersWithViews--> genellikle API senaryolar� i�in
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { //Geli�tirme (Development) a�amas�nda g�stermek i�in / Son kullan�c� g�rmez
            app.UseDeveloperExceptionPage();//exception lar� sayfada g�stermek i�in / developer i�in
            }

            //web sunucumuzu bir �eyler yapacak �ekilde yap�land�rmak istedi�imizi s�ylemek istiyoruz
            //her aramada bu cevab� almak yerine, bir �eyler yapmas�n� istemek i�in
            app.UseStaticFiles(); //web sunucusu ortaya ��kt���nda yapabileci�i bir �ey olarak statik dosyalar sunma hizmeti eklemesini s�yleriz

            app.UseRouting();//sunucuya gelen tek tek aramalr� belirli kod par�alar�na y�nlendirmeyi sa�lar. 

            //Endpoints --> sunucuya gelen istekleri kar��lamaya �al��mak i�in i�lenecek bir ara yaz�l�m k�mesi belirlememize olanak tan�r.
            app.UseEndpoints(cfg =>
            {
                //MapControllerRoute --> bir model olu�turmam�za izin verecektir
                cfg.MapControllerRoute("Default", //bu y�zden varsay�lan (default) yol olarak adland�r�ld�.
                    "/{controller}/{action}/{id?}", //sonra aranan / id? --> iste�e ba�l� (?, ista�e ba�l� oldu�unu g�sterir - null yap�labilir) 
                    new { controller = "App", action = "Index" });// anoni nesnede, varsay�lanlar�n ne oldu�u
                                                                  //Nedeni, belirtilmemi�se varsay�lanlar� vermek i�in.
                                                                  //Birisi controller eylem(action) ve kimlik(id) belirtilmemi� sayfan�n k�k�ne gitti�inde,
                                                                  //varsay�lan olarak AppController dizinine gitmesi s�yleniyor. 
            });
        }
    }
}
