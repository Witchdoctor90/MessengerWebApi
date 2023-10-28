using System.ComponentModel.DataAnnotations;

namespace MessengerWebApi.Models.Entities;

public class Contact
{
    public int Id { get; set; }
    public User User { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [Range(0,16)]
    public string Phone { get; set; }
    public string Email { get; set; }
    
    public DateTime CreatedAt { get; set; }
}