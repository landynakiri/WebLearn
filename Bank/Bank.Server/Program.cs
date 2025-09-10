
using Bank.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // -------------------- Identity 設定區塊 --------------------
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseInMemoryDatabase("AppDb"));
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // -------------------- Identity 路由 --------------------
            app.MapIdentityApi<IdentityUser>();
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

            app.Run();
        }
    }
}
