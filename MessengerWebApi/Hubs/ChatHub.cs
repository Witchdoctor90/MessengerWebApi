using MessengerWebApi.Clients;
using MessengerWebApi.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace MessengerWebApi.Hubs;

[SignalRHub]
public class ChatHub : Hub<IChatClient>
{
    public async Task Send(Message msg)
    {
        //todo replace with real function
        await Clients.All.ReceiveMessage(msg);
    }
}