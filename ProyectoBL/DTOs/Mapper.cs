using ProyectoBL.Models;
using AutoMapper;
using System;
namespace ProyectoBL.DTOs
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Area, AreaOutputDTO>();
            CreateMap<AreaDTO, Area>();
            CreateMap<Prioridad, PrioridadOutputDTO>();
            CreateMap<PrioridadDTO, Prioridad>();
            CreateMap<Encargado, EncargadoOutputDTO>();
            CreateMap<EncargadoDTO, Encargado>();
            CreateMap<Requerimiento, RequerimientoOutputDTO>();
            CreateMap<RequerimientoDTO, Requerimiento>();

        }
    }
}
