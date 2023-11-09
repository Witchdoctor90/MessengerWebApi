using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MessengerWebApi.Models.Entities;

public class User : IdentityUser
{

    public int Id { get; set; }
    
    [Range(0,16)]
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string? LastName { get; set; }
    
    public bool IsActive { get; set; }
    public bool IsReported { get; set; }
    public bool IsBlocked { get; set; }
    
    public string? Prefs { get; set; }
    public List<Contact> Contacts { get; set; } = new();
    public List<Conversation> Conversations { get; set; } = new();
    
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
}