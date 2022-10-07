using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class ProveedorRepositorio : Repositorio<Proveedor>, IProveedorRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ProveedorRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Proveedor proveedor)
        {
            var proveedorDb = _db.Proveedor.FirstOrDefault(p => p.Id == proveedor.Id);
            if (proveedorDb != null)
            {
                proveedorDb.Nit = proveedor.Nit;
                proveedorDb.NombreProveedor = proveedor.NombreProveedor;
                proveedorDb.Direccion = proveedor.Direccion;
                proveedorDb.Telefono = proveedor.Telefono;
                proveedorDb.Ciudad = proveedor.Ciudad;
                proveedorDb.Estado = proveedor.Estado;
                _db.SaveChanges();
            }
        }
    }
}