using MessengerWebApi.Models.Entities;

namespace MessengerWebApi.Clients;

public interface IChatClient
{
    Task ReceiveMessage(Message msg);
}