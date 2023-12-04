using System.Linq.Expressions;
using MessengerWebApi.Models.Entities;
using MessengerWebApi.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessengerWebApi.Models.Db.Repositories;

public class MessagesRepository : IAsyncRepository<Message>
{
    private readonly MessengerDbContext _db;

    public MessagesRepository(MessengerDbContext db)
    {
        _db = db;
    }


    public async Task<Message> GetById(int id) => await _db.Messages.FindAsync(id);


    public async Task<Message> FirstOrDefault(Expression<Func<Message, bool>> predicate) =>
        await _db.Messages.FirstOrDefaultAsync(predicate);


    public async Task Add(Message entity)
    {
        try
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Update(Message entity)
    {
        _db.Messages.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }

    public async Task Remove(Message entity)
    {
        _db.Messages.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Message>> GetAll() => await _db.Messages.ToListAsync();

    public async Task<IEnumerable<Message>> GetWhere(Expression<Func<Message, bool>> predicate) => await _db.Messages.Where(predicate).ToListAsync();

    public async Task<int> CountAll() => await _db.Messages.CountAsync();

    public async Task<int> CountWhere(Expression<Func<Message, bool>> predicate) => await _db.Messages.CountAsync(predicate);
}