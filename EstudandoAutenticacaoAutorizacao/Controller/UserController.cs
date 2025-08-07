using EstudandoAutenticacaoAutorizacao.Model;
using EstudandoAutenticacaoAutorizacao.Repository;
using EstudandoAutenticacaoAutorizacao.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EstudandoAutenticacaoAutorizacao.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet("getAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]   
    public async Task <IActionResult> GetUsers()
    {
        var users = await _userRepository.GetUsers();
        return Ok(users);        
    }

    [HttpPost("addUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task <IActionResult> AddUser(User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _userRepository.AddUser(user);
        return Ok("User inserted sucessfully");
                            
    }
}