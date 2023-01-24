using Business.Models.v1;
using Persistence.Entities.v1;
using AutoMapper;

namespace Business.AutoMapper.Profiles
{
    public class UpdateResponseProfile : Profile
    {
        public UpdateResponseProfile()
        {
            CreateMap<Vehicle, UpdateResponse>()
               .ForMember(x => x.Town, opt
                   => opt.MapFrom(x => x.Town.Name));
        }
    }
}
