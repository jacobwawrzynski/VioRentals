using AutoMapper;
using VioRentals.AuthAPI;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using VioRentals.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=../VioRentalsData.db"));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
