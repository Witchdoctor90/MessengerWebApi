using MessengerWebApi.Models.Entities;

namespace MessengerWebApi.Models.DTO;

public class IdentityUserDTO 
{
    public int Id { get; set; }
    public string Username { get; set; }
    public List<Contact> Contacts { get; set; }
    
    public List<ConversationDTO> Conversations { get; set; }
}