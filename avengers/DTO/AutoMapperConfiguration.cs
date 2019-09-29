using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using avengers.Models;

namespace avengers.DTO
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Comic, ComicDTO>()
                    .ForMember(x => x.Personaje, o => o.Ignore())
                    .ForMember(x => x.Creador, o=> o.Ignore())
                    .ReverseMap();

                cfg.CreateMap<Creador, CreadorDTO>()
                    .ReverseMap();

                cfg.CreateMap<Personaje, PersonajeDTO>()
                    .ReverseMap();
            });
        }
    }
}
