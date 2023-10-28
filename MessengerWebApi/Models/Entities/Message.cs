using MessengerWebApi.Models.Enums;

namespace MessengerWebApi.Models.Entities;

public class Message
{
    public int Id { get; set; }
    public string Guid { get; set; }
    
    public MessageType Type { get; set; }
    public string Body { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }

    private int ConversationId { get; set; }
    private int SenderId { get; set; }
    
}