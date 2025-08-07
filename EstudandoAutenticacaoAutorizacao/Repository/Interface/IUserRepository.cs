using EstudandoAutenticacaoAutorizacao.Model;

namespace EstudandoAutenticacaoAutorizacao.Repository.Interface;

public interface IUserRepository
{
    Task<List<User>> GetUsers();

    Task AddUser(User user);
}