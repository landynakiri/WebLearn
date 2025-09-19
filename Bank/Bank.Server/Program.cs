
using Bank.Server.Data;
using Bank.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // -------------------- Identity 設定區塊 --------------------
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.UseInMemoryDatabase("AppDb");
                }
                else if (builder.Environment.IsStaging())
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
                else if (builder.Environment.IsProduction())
                {
                    //記得改用真正的資料庫連接
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = builder.Environment.IsProduction() ? true : false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // -------------------- Identity 路由 --------------------
            //app.MapIdentityApi<IdentityUser>();
            app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager,
                [FromBody] object empty) =>
                {
                    if (empty != null)
                    {
                        await signInManager.SignOutAsync();
                        return Results.Ok();
                    }
                    return Results.Unauthorized();
                })
                .WithOpenApi()
                .RequireAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            CreateDbIfNotExists(app);

            using var scope = app.Services.CreateScope();
            await SeedData.Initialize(scope.ServiceProvider);

            await app.RunAsync();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.EnsureCreated();
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
