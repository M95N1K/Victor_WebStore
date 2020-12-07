using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Victor_WebStore.Clients.Employees;
using Victor_WebStore.Clients.Identity;
using Victor_WebStore.Clients.Order;
using Victor_WebStore.Clients.Products;
using Victor_WebStore.Clients.Values;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Interfaces.Services;
using Victor_WebStore.Interfaces.TestApi;
using Victor_WebStore.Services;
using Victor_WebStore.Logger;

namespace Victor_WebStore
{
    public sealed record Startup(IConfiguration Configuration)
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IEmployeesService, EmployeesClient>();
            services.AddScoped<IProductService, ProductsClient>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartStore, CoockiesCartStore>();
            services.AddScoped<IOrderService, OrderClients>();
            services.AddTransient<IValueService, ValuesClient>();

            services
                .AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders();

            services
               .AddTransient<IUserStore<User>, UsersClient>()
               .AddTransient<IUserRoleStore<User>, UsersClient>()
               .AddTransient<IUserPasswordStore<User>, UsersClient>()
               .AddTransient<IUserEmailStore<User>, UsersClient>()
               .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
               .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
               .AddTransient<IUserClaimStore<User>, UsersClient>()
               .AddTransient<IUserLoginStore<User>, UsersClient>();

            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();


            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "VWebStore";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenaied";
                options.SlidingExpiration = true;

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints для областей
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
