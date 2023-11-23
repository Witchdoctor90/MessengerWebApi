namespace MessengerWebApi.Models.Entities;

public class Connection
{
    public string ConnectionID { get; set; }
    public User User { get; set; }
    public string UserAgent { get; set; }
    public bool Connected { get; set; }
}