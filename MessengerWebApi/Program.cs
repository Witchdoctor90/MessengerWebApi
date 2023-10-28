using MessengerWebApi.Hubs;
using MessengerWebApi.Models.Db;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR();

builder.Services.AddDbContext<MessengerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection")));

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
app.UseEndpoints(endpoints 
    => endpoints.MapHub<ChatHub>("/hubs/chat"));
app.Run();