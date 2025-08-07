using EstudandoAutenticacaoAutorizacao.Model;

namespace EstudandoAutenticacaoAutorizacao.Repository.Interface;

public interface IUserRepository
{
    Task<List<User>> GetUsers();

    Task<User> GetUserById(int id);

    Task AddUser(User user);
}