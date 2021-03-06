using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
             {
                 cfg.User.RequireUniqueEmail = true; //birden fazla kullan?c? ayn? e-postaya sahip olamaz.
             })
                .AddEntityFrameworkStores<DutchContext>(); //veritaban?nda depolanan farkl? nesnelere ula?mak istedi?inde
                                                           //ona kimlik i?inde dahil olarak kullnaca?? ba?lam t?r?n? s?yler
            
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg=>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer=false,
                        ValidateAudience=false,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                    };
                });
            
            services.AddDbContext<DutchContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("DutchContextDb"));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());            
            services.AddTransient<DutchSeeder>();
            services.AddScoped<IDutchRepository, DutchRepository>();
            services.AddTransient<IMailService, NullMailService>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddRazorPages();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            { //Geli?tirme (Development) a?amas?nda g?stermek i?in / Son kullan?c? g?rmez
                app.UseDeveloperExceptionPage();//exception lar? sayfada g?stermek i?in / developer i?in
            }
            else
            {
                //development a?amas?nda olmad??? zaman 
                //istisnay? yakalayan bir pipeline a sahip olmam?z? sa?lar
                //bizim i?in otomatik g?nl??e kaydeder
                app.UseExceptionHandler("/error"); //error --> istisnay? verece?i sayfa
                //bunun i?in controller veya bir controlle da bir action(eylem) olu?turulmayacak
                //Razor Pages kullan?lacak
            }

            //web sunucumuzu bir ?eyler yapacak ?ekilde yap?land?rmak istedi?imizi s?ylemek istiyoruz
            //her aramada bu cevab? almak yerine, bir ?eyler yapmas?n? istemek i?in
            app.UseStaticFiles(); //web sunucusu ortaya ??kt???nda yapabileci?i bir ?ey olarak statik dosyalar sunma hizmeti eklemesini s?yleriz

            app.UseRouting();//sunucuya gelen tek tek aramalr? belirli kod par?alar?na y?nlendirmeyi sa?lar. 

            app.UseAuthentication();
            app.UseAuthorization();

            //Endpoints --> sunucuya gelen istekleri kar??lamaya ?al??mak i?in i?lenecek bir ara yaz?l?m k?mesi belirlememize olanak tan?r.
            app.UseEndpoints(cfg =>
            {

                cfg.MapRazorPages(); //MapRazorPages diyerek onuda se?memiz gerekir
                                     //ve bu sadece derkii, varsay?lan sayfay?,
                                     //sahip olunan view ad?n? ve razor sayfalar? kullan?lacak

                //MapControllerRoute --> bir model olu?turmam?za izin verecektir
                cfg.MapControllerRoute("Default", //bu y?zden varsay?lan (default) yol olarak adland?r?ld?.
                    "/{controller}/{action}/{id?}", //sonra aranan / id? --> iste?e ba?l? (?, ista?e ba?l? oldu?unu g?sterir - null yap?labilir) 
                    new { controller = "App", action = "Index" });// anoni nesnede, varsay?lanlar?n ne oldu?u
                                                                  //Nedeni, belirtilmemi?se varsay?lanlar? vermek i?in.
                                                                  //Birisi controller eylem(action) ve kimlik(id) belirtilmemi? sayfan?n k?k?ne gitti?inde,
                                                                  //varsay?lan olarak AppController dizinine gitmesi s?yleniyor. 
            });
        }
    }
}
