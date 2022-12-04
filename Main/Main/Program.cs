using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Main.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MainContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainContext") ?? throw new InvalidOperationException("Connection string 'MainContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); //redirects http request to https
app.UseStaticFiles();//enables static filesm like html, css, images and js.

app.UseRouting();//adds rout matching to the middleware pipeline.

app.UseAuthorization();//autorizes a user to acces secure resources

app.MapRazorPages();//configures endpoint routing for razor pages.

app.Run();//runs the app
