using EstudandoAutenticacaoAutorizacao.Model;

namespace EstudandoAutenticacaoAutorizacao.Repository.Interface;

public interface IUserRepository
{
    Task<List<User>> GetUsers();

    Task<User> GetUserByEmail(string email);

    Task AddUser(User user);
}