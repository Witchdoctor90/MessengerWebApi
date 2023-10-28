using Microsoft.EntityFrameworkCore;

namespace MessengerWebApi.Models.Db;

public class MessengerDbContext : DbContext
{
    public MessengerDbContext(DbContextOptions<MessengerDbContext> options) 
        : base(options)
    {
        
    }
    
    
}