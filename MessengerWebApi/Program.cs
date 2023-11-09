using MessengerWebApi.Hubs;
using MessengerWebApi.Models.Db;
using MessengerWebApi.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddDbContext<MessengerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection")));


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MessengerDbContext>();


builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000")
            .AllowCredentials();
    });
});


var app = builder.Build();

app.UseCors("ClientPermission");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints 
    => endpoints.MapHub<ChatHub>("/hubs/chat"));
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();