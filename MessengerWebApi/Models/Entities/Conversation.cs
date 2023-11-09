using System.ComponentModel.DataAnnotations;

namespace MessengerWebApi.Models.Entities;

public class Conversation
{

    public int Id { get; set;}
    
    [Range(0,40)]
    public string Title { get; set; }
    public int CreatorId { get; set; }
    public int ChannelId { get; set; }

    public List<User> Participants { get; set; } = new();
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}