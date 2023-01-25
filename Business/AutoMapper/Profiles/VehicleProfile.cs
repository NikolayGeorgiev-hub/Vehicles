using AutoMapper;
using Business.Models.v1.Vehicles;
using Persistence.Entities.v1;

namespace Business.AutoMapper.Profiles
{
    internal class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleResponse>()
                .ForMember(v => v.Town, opt
                    => opt.MapFrom(t => $"{t.Town.Name} {t.Town.Postcode}"))
                .ForMember(x => x.Purpose, opt 
                    => opt.MapFrom(x => $"{x.Purpose} - {x.VehicleType.Type}"));

        }

    }
}
