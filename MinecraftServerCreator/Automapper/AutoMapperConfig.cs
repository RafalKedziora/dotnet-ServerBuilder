using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerCreator.Automapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
    => new MapperConfiguration(cfg =>
    {
        #region AM ready

        #endregion

    }).CreateMapper();
    }
}
