using MusicEvents.Data;
using Microsoft.EntityFrameworkCore;

namespace MusicEvents.Repositories;

public class EventRepository
{
    private readonly EntityContext _context;

    public EventRepository(EntityContext context)
    {
        _context = context;
    }

    public void Create(Music _event)
    {
        _context.Events.Add(_event);
        _context.SaveChanges();
    }

    public List<Music> GetAllFromUser(Guid id)
    {
        var res = _context.Events
            .Where(Music => Music.UserId == id)
            .ToList();

        return res;
    }

    public List<Music> GetPublic()
    {
        var res = _context.Events
            .Include(Music=>Music.User)
            .Where(Music => Music.IsPublic)
            .ToList();

        return res;
    }

    public Music? GetById(Guid id)
    {
        var res = _context.Events
            .FirstOrDefault(Music => Music.Id == id);

        return res;
    }

    public void Update(Music _event)
    {
        _context.Events.Update(_event);
        _context.SaveChanges();
    }

    public void Delete(Music _event)
    {
        _context.Events.Remove(_event);
        _context.SaveChanges();
    }
}