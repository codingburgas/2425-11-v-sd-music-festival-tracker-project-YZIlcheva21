using MusicFestivalManagementSystem.Data;
using MusicFestivalManagementSystem.Models;
using System.Linq;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public User? Authenticate(string username, string password)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }
}
