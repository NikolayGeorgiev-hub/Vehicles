using AutoMapper;
using Business.Models.v1.Vehicles;
using Persistence.Entities.v1.Vehicles;

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
