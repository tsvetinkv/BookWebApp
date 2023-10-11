using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookWebApp.Utilities.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, string>().ConvertUsing(str => !string.IsNullOrEmpty(str) ? str.Trim() : null);

            var mapFromType = typeof(IMapFrom<>);
            var mapToType = typeof(IMapTo<>);

            var modelRegistrations = AppDomain
                              .CurrentDomain
                              .GetAssemblies()
                              .Where(a => a.GetName().Name.StartsWith("BookWebApp"))
                              .SelectMany(a => a.GetExportedTypes())
                              .Where(t => t.IsClass && !t.IsAbstract)
                              .Select(t => new
                              {
                                  Type = t,
                                  MapFrom = GetMappingModel(t, mapFromType),
                                  MapTo = GetMappingModel(t, mapToType)
                              });

            foreach (var modelRegistration in modelRegistrations)
            {
                if (modelRegistration.MapFrom != null && modelRegistration.MapFrom.Any())
                {
                    modelRegistration.MapFrom.ForEach(m => CreateMap(m, modelRegistration.Type));
                }

                if (modelRegistration.MapTo != null && modelRegistration.MapTo.Any())
                {
                    modelRegistration.MapTo.ForEach(m => CreateMap(modelRegistration.Type, m));
                }
            }
        }

        private List<Type> GetMappingModel(Type type, Type mappingInterface)
        {
            return type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == mappingInterface)
              .Select(s => s.GetGenericArguments()?.First()).ToList();
        }
    }
}
