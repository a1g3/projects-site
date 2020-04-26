using Alge.Domain.Dtos;
using Alge.ViewModels;
using Autofac;
using AutoMapper;

namespace Alge.Helpers
{
    public class MapperProvider : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var config = CreateConfiguration();
            builder.RegisterInstance(config).As<MapperConfiguration>().SingleInstance();
            builder.RegisterInstance(new Mapper(config)).As<IMapper>().SingleInstance();
        }

        public static MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<OcspDto, OcspStatusViewModel>()
                    .ForMember(x => x.Error, opt => opt.Ignore());
            });

            config.AssertConfigurationIsValid();

            return config;
        }
    }
}
