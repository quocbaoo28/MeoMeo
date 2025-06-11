using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Đọc config
var config = builder.Configuration;

// JWT cho Gateway (để xác thực người dùng trước khi gọi tiếp downstream)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Secret"]))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddOcelot(); // Bắt buộc

var app = builder.Build();

// Cấu hình middleware
app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot(); // Bắt buộc và phải đặt cuối

app.Run();
