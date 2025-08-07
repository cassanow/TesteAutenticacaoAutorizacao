using EstudandoAutenticacaoAutorizacao.Model;

namespace EstudandoAutenticacaoAutorizacao.Service;

public interface ITokenService
{
    Task<string> GenerateToken(User user); 
}