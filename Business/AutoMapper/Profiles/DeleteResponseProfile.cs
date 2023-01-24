using AutoMapper;
using Business.Models.v1;
using Persistence.Entities.v1;

namespace Business.AutoMapper.Profiles
{
    public class DeleteResponseProfile : Profile
    {
        public DeleteResponseProfile()
        {
            CreateMap<Vehicle, DeleteRespons>()
                .ForMember(x => x.VehicleType, opt 
                    => opt.MapFrom(x => x.VehicleType.Type));
        }

    }
}
