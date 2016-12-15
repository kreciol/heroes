using AutoMapper;


public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<Models.Hero, ViewModels.Hero>();
        CreateMap<ViewModels.Hero, Models.Hero>()
            .ForMember(h => h.Id, opt => opt.Ignore());
    }
}