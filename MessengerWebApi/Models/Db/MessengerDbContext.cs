using MessengerWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessengerWebApi.Models.Db;

public class MessengerDbContext : DbContext
{
    public MessengerDbContext(DbContextOptions<MessengerDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Conversation> Conversations { get; set; }

}