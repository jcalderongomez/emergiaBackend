using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IDetalleVentaRepositorio DetalleVenta { get; private set; }
        public IProductoRepositorio Producto { get; private set; }
        public IProveedorRepositorio Proveedor { get; private set; }
        public IRolRepositorio Rol { get; private set; }
        public IUsuarioAplicacionRepositorio UsuarioAplicacion { get; private set; }
        public IVentaRepositorio Venta { get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            DetalleVenta = new DetalleVentaRepositorio(_db);
            Producto = new ProductoRepositorio(_db);
            Proveedor = new ProveedorRepositorio(_db);
            Rol = new RolRespositorio(_db);
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
            Venta = new VentaRepositorio(_db);
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
