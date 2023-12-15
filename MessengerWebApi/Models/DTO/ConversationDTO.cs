namespace MessengerWebApi.Models.DTO;

public class ConversationDTO
{
    public string Title { get; set; }
    public string ChannelId { get; set; }
    public List<int> ParticipantsId { get; set; } = new();
}