using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Repositories;
using VioRentals.Web.DTOs.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => 
	options.UseSqlite("Data Source=../VioRentalsData.db"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

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
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
