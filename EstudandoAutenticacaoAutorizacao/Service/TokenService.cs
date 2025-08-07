using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EstudandoAutenticacaoAutorizacao.Model;
using EstudandoAutenticacaoAutorizacao.Repository.Interface;
using Microsoft.IdentityModel.Tokens;

namespace EstudandoAutenticacaoAutorizacao.Service;

public class TokenService : ITokenService   
{
    private readonly IConfiguration _configuration; 
    private readonly IUserRepository _userRepository;

    public TokenService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }
    
    public async Task<string> GenerateToken(User user)
    {
        var userDataBase = await _userRepository.GetUserById(user.Id);
        
        if(user.Name != userDataBase.Name && user.Name != userDataBase.Password) 
            return String.Empty;  
        
        var secretKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var issuer = _configuration["JWT:Issuer"];      
        var audience = _configuration["JWT:Audience"];     
        
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: new[]
            {
                new Claim(type: ClaimTypes.Name, value: user.Name),
            },
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signingCredentials
        );
        
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);   
        
        return token;
    }
}