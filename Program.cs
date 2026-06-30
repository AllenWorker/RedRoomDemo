using RedRoomDemo.Database;
using RedRoomDemo.Data.Database;
using RedRoomDemo.Data.Repositories.Implementations;
using RedRoomDemo.Data.Repositories.Interfaces;
using RedRoomDemo.Services.Implementations;
using RedRoomDemo.Services.Interfaces;

namespace RedRoomDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register application services and repositories with ASP.NET Core built-in DI container.
            builder.Services.AddScoped<IDbConnectionFactory, SqliteConnectionFactory>();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            // Workshop purpose: keep startup-time database initialization to simulate a common legacy pattern.
            LegacyDatabaseInitializer.Initialize(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
