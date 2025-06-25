using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Services;
using OnlineLearning.DAL.Context;
using OnlineLearning.DAL.Repositories.Abstracts;
using OnlineLearning.DAL.Repositories.Concretes;
using System;

namespace Lahiye.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

            // Stripe API açarını təyin et
            var stripeSettings = builder.Configuration.GetSection("Stripe").Get<StripeSettings>();
            Stripe.StripeConfiguration.ApiKey = stripeSettings.SecretKey;

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<PaymentService>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default")
                    
                ));

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<INewsEventService, NewsEventService>();
            builder.Services.AddScoped<INewsEventRepository, NewsEventRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ICafeMenuRepository, CafeMenuRepository>();
            builder.Services.AddScoped<ICafeMenuService, CafeMenuService>();
            
            builder.Services.AddScoped<IPaidBookRepository, PaidBookRepository>();
            builder.Services.AddScoped<IPaidBookService, PaidBookService>();

            builder.Services.AddScoped<ICartRepository,CartRepository>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IFavoriteBookRepository, FavoriteBookRepository>();
            builder.Services.AddScoped<IFavoriteBookService, FavoriteBookService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ILibraryItemRepository, LibraryItemRepository>();
            builder.Services.AddScoped<ILibraryItemService, LibraryItemService>();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();   
            app.UseAuthorization();
            app.UseRouting();

            app.UseSession();



            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseAuthentication();

            app.UseAuthorization();

            app.Run();
        }
    }
}
