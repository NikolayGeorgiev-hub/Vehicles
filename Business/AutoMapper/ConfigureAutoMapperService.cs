using AutoMapper;
using Business.AutoMapper.Profiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Vehicles.Business.AutoMapper
{
    public static class ConfigureAutoMapperServices
    {
        public static IMapper ConfigureAutomapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new VehicleProfile());
                configuration.AddProfile(new TownProfile());
                configuration.AddProfile(new DeleteResponseProfile());
                configuration.AddProfile(new UpdateResponseProfile());
            });

            var mapper = config.CreateMapper();
            services.TryAddSingleton(mapper);
            return mapper;
        }
    }
}
