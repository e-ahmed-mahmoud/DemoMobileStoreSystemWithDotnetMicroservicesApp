﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.mapping
{
    public static class OrderMapper
    {
        private static Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
               {
                   cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                   cfg.AddProfile<OrderMapperProfile>();
               });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
