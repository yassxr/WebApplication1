using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Hubs;
using WebApplication1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
/*builder.Services.AddIdentityCore<AppUser>()
               .AddDefaultTokenProviders(UIFramework.Bootstrap5)
               .AddEntityFrameworkStores<ApplicationDbContext>();*/

builder.Services.AddIdentityCore<AppUser>()
               .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
/*app.UseSignalR(route =>
{
    route.MapHub<ChatHub>("/Home/Index");
}*/

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/Home/Index");
}
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
