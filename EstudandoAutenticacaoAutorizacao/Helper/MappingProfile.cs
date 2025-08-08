using AutoMapper;
using EstudandoAutenticacaoAutorizacao.DTO;
using EstudandoAutenticacaoAutorizacao.Model;

namespace EstudandoAutenticacaoAutorizacao.Helper;

public class MappingProfile : Profile   
{
    public MappingProfile()
    {
        CreateMap<User, LoginDTO>().ReverseMap();
    }
}