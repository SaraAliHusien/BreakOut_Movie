
using BreakOut_Movie.Models;
using BreakOut_Movie.Models.Repo.Absrtraction;
using BreakOut_Movie.Models.Repo.Implementation;
using BreakOut_Movie.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using NToastNotify;

namespace BreakOut_Movie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<BreakOut_DbContext>
            (options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                PreventDuplicates=true,
                CloseButton = true,

            }) ;
            builder.Services.AddControllersWithViews()
             .AddJsonOptions(options =>
                 options.JsonSerializerOptions.PropertyNamingPolicy = null);
           
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BreakOut_DbContext>();
            builder.Services.AddScoped<IMovieRepo, MovieRepo>();
            builder.Services.AddScoped<IGenreRepo, GenreRepo>();
            builder.Services.AddScoped<IViewRenderService, ViewRenderService>();
            builder.Services.AddScoped<IRoleRepo, RoleRepo>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Account/Index";
                //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
         
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}