using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dto;
using Core.Entidades;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Rol, RolDto>().ReverseMap();

            CreateMap<Proveedor, ProveedorUpsertDto>().ReverseMap();

            CreateMap<Producto, ProductoUpsertDto>().ReverseMap();

            CreateMap<Producto, ProductoReadDto>()
            .ForMember(producto => producto.Proveedor, proveedor =>
            proveedor.MapFrom(c => c.Proveedor.NombreProveedor));

            // CreateMap<UsuarioAplicacion, UsuarioAplicacionReadDto>()
            // .ForMember(u => u.Rol, r =>
            // r.MapFrom(c => c.Rol.NombreRol));
        }

    }
}