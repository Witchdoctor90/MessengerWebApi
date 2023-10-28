using System.ComponentModel.DataAnnotations;

namespace MessengerWebApi.Models.Entities;

public class User
{
    public int Id { get; set; }
    
    [Range(0,16)]
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public bool IsActive { get; set; }
    public bool IsReported { get; set; }
    public bool IsBlocked { get; set; }
    
    public string Prefs { get; set; }
    public List<Contact> Contacts { get; set; }
    public List<Conversation> Conversations { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
}