using AutoMapper;
using EstudandoAutenticacaoAutorizacao.DTO;
using EstudandoAutenticacaoAutorizacao.Model;
using EstudandoAutenticacaoAutorizacao.Service;
using Microsoft.AspNetCore.Mvc;

namespace EstudandoAutenticacaoAutorizacao.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthController(ITokenService tokenService, IMapper mapper)
    {
        _tokenService = tokenService;
        _mapper = mapper;
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var user = _mapper.Map<User>(loginDTO);
        var token = await _tokenService.GenerateToken(user);

        if (token == null)
            return Unauthorized();

        return Ok(token);
    }
}