using MessengerWebApi.Models.Entities;

namespace MessengerWebApi.Clients;

public interface IChatClient
{
    Task ReceiveMessage(Message msg);
    Task ReceiveByUsername(Message msg, string receiverUsername);
    Task ReceiveFrom(Message msg, string user);
}