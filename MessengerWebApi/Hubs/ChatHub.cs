using MessengerWebApi.Clients;
using MessengerWebApi.Models.Db;
using MessengerWebApi.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSwaggerGen.Attributes;

namespace MessengerWebApi.Hubs;

[SignalRHub]
[Authorize]
public class ChatHub : Hub<IChatClient>
{
    private readonly MessengerDbContext _db;

    public ChatHub(MessengerDbContext db)
    {
        _db = db;
    }


    public async Task Send(Message msg)
    {
        var sender = await _db.Users.FirstOrDefaultAsync(u => u.FirstName == Context.User.Identity.Name);
        if (sender is null) return;
        await Clients.All.ReceiveFrom(msg, sender.FirstName);
    }

    public override async Task OnConnectedAsync()
    {
        var name = Context.User.Identity.Name;
        var user = await _db.Users.Include(u => u.Connections)
            .FirstOrDefaultAsync(u => u.FirstName == name);
        
        if (user != null)
        {
            var connection = new Connection
            {
                Connected = true,
                ConnectionID = Context.ConnectionId,
                User = user,
                UserAgent = Context.GetHttpContext().Request.Headers.UserAgent
            };
            user.Connections.Add(connection);
            await _db.SaveChangesAsync();
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = await _db.Connections.FirstOrDefaultAsync(c => c.ConnectionID == Context.ConnectionId);
        connection.Connected = false;
        
        await base.OnDisconnectedAsync(exception);
    }
}