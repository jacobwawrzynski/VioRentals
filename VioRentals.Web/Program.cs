using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Repositories;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Web.DTOs.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => 
	options.UseSqlite("Data Source=../VioRentalsData.db"));

// EF repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IRentalService, RentalService>();
builder.Services.AddTransient<IMembershipService, MembershipService>();
builder.Services.AddTransient<IGenreService, GenreService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = 
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("01234567890ABCDEF0123456789ABCDEF01234567890ABCDEF0123456789ABCDEF")
                )
        };

        //options.Events = new JwtBearerEvents
        //{
        //    OnChallenge = context =>
        //    {
        //        context.Response.Redirect("/Home/Login");
        //        context.HandleResponse();
        //        return Task.CompletedTask;
        //    }
        //};
    });

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

//app.Use(async (context, next) =>
//{
//    if ((bool)!context.User.Identity?.IsAuthenticated)
//    {
//        context.Response.Redirect("/Home/Login");
//        return;
//    }

//    context.Response.Redirect("Customer/Index");
//    return;
//    await next.Invoke();
//});

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
