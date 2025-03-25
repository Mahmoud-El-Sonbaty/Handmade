using Handmade.Application.Mapper;
using Handmade.Application.Services.AuthServices;
using Handmade.Application.Services.CloudinaryServices;
using Handmade.Application.Services.EmailServices;
using Handmade.Context;
using Handmade.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace Handmade.ClientWebsiteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<HandmadeContext>(op => 
            op.UseNpgsql(connectionString));
            //builder.Services.AddMapster();
            // Configure Mapster
            MapsterConfig.Configure();
            // Register PasswordRequirementsConfig as IPasswordRequirementsConfig
            //builder.Services.Configure<IPasswordRequirementsConfig>(builder.Configuration.GetSection("PasswordRequirements"));
            //builder.Services.AddSingleton<PasswordValidationAttribute>();
            builder.Services.AddControllers();
            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            // Add services to the container.

            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("PasswordRequirements:RequireConfirmedEmail");
                options.Password.RequireDigit = builder.Configuration.GetValue<bool>("PasswordRequirements:RequireDigit");
                options.Password.RequiredLength = builder.Configuration.GetValue<int>("PasswordRequirements:MinimumLength");
                options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("PasswordRequirements:RequireSpecialCharacter");
                options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("PasswordRequirements:RequireUppercase");
                options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("PasswordRequirements:RequireLowercase");
                options.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("PasswordRequirements:RequireUniqueEmail");
            })
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<HandmadeContext>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
            builder.Services.AddAuthentication(op =>
            {
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
                };
            });
            builder.Services.AddCors(op =>
            {
                op.AddPolicy("Default", policy =>
                {
                    //policy.WithOrigins("http://localhost:4200", "http://anydomain:domainport", "null")
                    //.WithHeaders("Authorization")
                    //.WithMethods("Post", "Get");
                    policy.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
                op.AddPolicy("Production", policy =>
                {
                    policy.WithOrigins("https://allbirds-git-elghoul-mahmoud-elsonbatys-projects.vercel.app", "https://allbirds-orcin.vercel.app", "http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            app.UseHttpsRedirection();
            app.UseCors("Default");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
