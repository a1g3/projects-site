using Alge.Domain.Dtos;
using Alge.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alge.Helpers
{
    public static class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<OcspDto, OcspStatusViewModel>()
                    .ForMember(x => x.Error, opt => opt.Ignore());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
