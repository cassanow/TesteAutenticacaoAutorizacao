using EstudandoAutenticacaoAutorizacao.Model;
using Microsoft.EntityFrameworkCore;

namespace EstudandoAutenticacaoAutorizacao.DataBase;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }  
}