using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartScheduler.Data;
using SmartScheduler.Data.Models;
using SmartScheduler.Services;

namespace SmartScheduler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔹 Configurare baza de date PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

            // 🔹 Adaugă ASP.NET Core Identity
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                // Configurare parole
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // 🔹 Înregistrează RoleManager în DI
            builder.Services.AddScoped<RoleManager<IdentityRole>>();

            // 🔹 Citește și verifică setările JWT din appsettings.json
            var jwtKey = builder.Configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
            {
                throw new Exception("JWT Key is missing or too short in appsettings.json. It must be at least 32 characters.");
            }

            var jwtIssuer = builder.Configuration["Jwt:Issuer"]
                ?? throw new Exception("JWT Issuer is missing in appsettings.json");

            var jwtAudience = builder.Configuration["Jwt:Audience"]
                ?? throw new Exception("JWT Audience is missing in appsettings.json");

            var key = Encoding.UTF8.GetBytes(jwtKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine($"Authentication challenge: {context.ErrorDescription}");
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder => builder.WithOrigins("https://yourfrontend.com") // Specify your frontend origin here
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedRolesAsync(roleManager);
            }

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigins"); 
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "User", "Admin", "Manager" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}