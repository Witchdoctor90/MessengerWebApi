using MessengerWebApi.Hubs;
using MessengerWebApi.Models.Db;
using MessengerWebApi.Models.Db.Repositories;
using MessengerWebApi.Models.Entities;
using MessengerWebApi.Models.Interfaces;
using MessengerWebApi.Models.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


IdentityModelEventSource.ShowPII = true;
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddDbContext<MessengerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection")));


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MessengerDbContext>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JWTAuthOptions.ISSUER,
 
            ValidateAudience = true,
            ValidAudience = JWTAuthOptions.AUDIENCE,
            ValidateLifetime = true,
 
            IssuerSigningKey = JWTAuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (string.IsNullOrEmpty(accessToken) ||
                    (!path.StartsWithSegments("/hubs/chat"))) return Task.CompletedTask;
                // Read the token out of the query string
                context.Token = accessToken;
                return Task.CompletedTask;
            }
        };
        options.RequireHttpsMetadata = false;
        options.Configuration = new OpenIdConnectConfiguration();
    });


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

builder.Services.AddScoped(typeof(IAsyncRepository<>),typeof(MessagesRepository));


var app = builder.Build();

app.UseCors("ClientPermission");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/hubs/chat", options =>
{
    options.Transports = HttpTransportType.WebSockets;
});

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();