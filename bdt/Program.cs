using bdt.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace bdt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ExpensesDbContext>(options =>
                options.UseMySql("server=localhost;database=bad decision tracker;user=Bid Gavi;password=Bid Gavi",
                new MySqlServerVersion(new Version(8, 0, 34)))
            );

            //builder.Services.AddDbContext<ExpensesDbContext>(options => options.UseInMemoryDatabase("BadDecisionTrackerDb"));

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Expenses}/{id?}");

            app.Run();
        }
    }
}
