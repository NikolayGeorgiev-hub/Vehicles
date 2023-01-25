using AutoMapper;
using Business.Models.v1.Towns;
using Business.Models.v1.Vehicles;
using Persistence.Entities.v1;

namespace Business.AutoMapper.Profiles
{
    public class TownProfile : Profile
    {
        public TownProfile()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new VehicleProfile()) );

            CreateMap<Town, TownResponse>()
                .ForMember(dest => dest.Vehicles, opt
                    => opt.MapFrom(x => x.Vehicles.Select(v => new Mapper(config).Map<VehicleResponse>(v))));
        }

    }
}
