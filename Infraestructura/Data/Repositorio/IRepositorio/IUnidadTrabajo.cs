using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        IDetalleVentaRepositorio DetalleVenta { get; }
        IProductoRepositorio Producto { get; }
        IProveedorRepositorio Proveedor { get; }
        IRolRepositorio Rol { get; }
        IUsuarioAplicacionRepositorio UsuarioAplicacion { get; }
        IVentaRepositorio Venta { get; }
        Task Guardar();
    }
}   