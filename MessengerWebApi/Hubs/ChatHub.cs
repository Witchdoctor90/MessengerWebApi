using MessengerWebApi.Clients;
using MessengerWebApi.Models.Db;
using MessengerWebApi.Models.Entities;
using MessengerWebApi.Models.Interfaces;
using MessengerWebApi.Models.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSwaggerGen.Attributes;

namespace MessengerWebApi.Hubs;

[SignalRHub]
[Authorize]
public class ChatHub : Hub<IChatClient>
{
    private readonly MessengerDbContext _db;
    private readonly IIdProvider _idProvider;

    public ChatHub(MessengerDbContext db, IIdProvider idProvider)
    {
        _db = db;
        _idProvider = idProvider;
    }
    

    public async Task CreateConversation(string title, List<string> participantsUsernames)
    {
        var creator = await _db.Users
            .Include(u => u.Connections)
            .FirstOrDefaultAsync(u => u.FirstName == Context.User.Identity.Name);
        var participants = new List<User>();
        foreach (var userName in participantsUsernames)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.FirstName == userName);
            participants.Add(user);
        }

        var conversation = new Conversation()
        {
            Title = title,
            ChannelId = _idProvider.GenerateId(),
            CreatedAt = DateTime.Now,
            CreatorId = creator.Id,
            UpdatedAt = DateTime.Now
        };

        foreach (var participant in participants)
        {
            conversation.Participants.Add(participant);
        }

        _db.Conversations.Add(conversation);
        await _db.SaveChangesAsync();

        foreach (var con in creator.Connections)
        {
            await Clients.Client(con.ConnectionID).UpdateConversations();
        }
    }

    public async Task<IActionResult> SendToConversation(Message msg)
    {
        throw new NotImplementedException();
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