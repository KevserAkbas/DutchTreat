using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DutchContext>(cfg => 
            {
                cfg.UseSqlServer(_config.GetConnectionString("DutchContextDb"));
            });
            
            services.AddTransient<IMailService,NullMailService>();
            services.AddScoped<IDutchRepository, DutchRepository>();
            services.AddTransient<DutchSeeder>();
            services.AddMvc();

            //AddControllersWithViews--> genellikle API senaryolarý için
            //services.AddControllersWithViews();

            ////Çalýþma zamaný derlemesi
            //services.AddRazorPages().AddRazorRuntimeCompilation();
            //// AddRazorRuntimeCompilation --> sisteme Razor sayfasýnýn deðiþtiði isteklerde Razor Sayfalarýný yeniden derlemesini söylemek içindir.

            //services.AddRazorPages();
            ////        Startup da bunu seçmek gerekir, varsayýlan olarak görünümlere sahip denetleyiciler eklemek yeterli deðildir.
            ////Ayrýca services.AddRazorPages() eklemek gerekli -bu, razor sayfalrý için ihtiyacýmýz olan tüm parçalarý ekler.
            ////
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            { //Geliþtirme (Development) aþamasýnda göstermek için / Son kullanýcý görmez
                app.UseDeveloperExceptionPage();//exception larý sayfada göstermek için / developer için
            }
            else
            {
                //development aþamasýnda olmadýðý zaman 
                //istisnayý yakalayan bir pipeline a sahip olmamýzý saðlar
                //bizim için otomatik günlüðe kaydeder
                app.UseExceptionHandler("/error"); //error --> istisnayý vereceði sayfa
                //bunun için controller veya bir controlle da bir action(eylem) oluþturulmayacak
                //Razor Pages kullanýlacak
            }

            //web sunucumuzu bir þeyler yapacak þekilde yapýlandýrmak istediðimizi söylemek istiyoruz
            //her aramada bu cevabý almak yerine, bir þeyler yapmasýný istemek için
            app.UseStaticFiles(); //web sunucusu ortaya çýktýðýnda yapabileciði bir þey olarak statik dosyalar sunma hizmeti eklemesini söyleriz

            app.UseRouting();//sunucuya gelen tek tek aramalrý belirli kod parçalarýna yönlendirmeyi saðlar. 

            //Endpoints --> sunucuya gelen istekleri karþýlamaya çalýþmak için iþlenecek bir ara yazýlým kümesi belirlememize olanak tanýr.
            app.UseEndpoints(cfg =>
            {

                cfg.MapRazorPages(); //MapRazorPages diyerek onuda seçmemiz gerekir
                                       //ve bu sadece derkii, varsayýlan sayfayý,
                                       //sahip olunan view adýný ve razor sayfalarý kullanýlacak

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
