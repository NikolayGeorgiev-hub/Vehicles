using AutoMapper;
using Business.Models.v1.Towns;
using Persistence.Entities.v1;

namespace Business.AutoMapper.Profiles
{
    public class TownProfile : Profile
    {
        public TownProfile()
        {
            CreateMap<Town, TownResponse>().ForMember(x => x.Vehicles, opt
                => opt.MapFrom(x => x.Vehicles));
        }

    }
}
