using EstudandoAutenticacaoAutorizacao.DataBase;
using EstudandoAutenticacaoAutorizacao.Model;
using EstudandoAutenticacaoAutorizacao.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EstudandoAutenticacaoAutorizacao.Repository;

public class UserRepository : IUserRepository
{
    private readonly Context _context;

    public UserRepository(Context context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);   
        await _context.SaveChangesAsync();
    }
}