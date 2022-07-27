using Application.Command.RegisterUser;
using AutoMapper;
using Domain.Entities;
using Shared.Lib.EventBus.AuthService;

namespace Application.Command.Mapping;

public static class AutoMapping
{
    private static readonly Lazy<IMapper> Lazy = new(() =>
    {
        var config = new MapperConfiguration(cfg => {
            // This line ensures that internal properties are also mapped over.
            cfg.ShouldMapProperty = p => p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly);
            cfg.AddProfile<MappingProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => Lazy.Value;
}
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegUserCommandInput, AccountRegisteredEvent>();
        CreateMap<RegUserCommandInput, Account>();
            
    }
}