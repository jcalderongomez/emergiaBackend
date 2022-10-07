using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            var productoDb = _db.Producto.FirstOrDefault(p => p.Id == producto.Id);
            if (productoDb != null)
            {
                if (producto.ImagenUrl != null)
                {
                    productoDb.ImagenUrl = producto.ImagenUrl;
                }

                productoDb.NumeroSerie = producto.NumeroSerie;
                productoDb.Descripcion = producto.Descripcion;
                productoDb.Precio = producto.Precio;
                productoDb.Costo = producto.Costo;
                productoDb.Cantidad = producto.Cantidad;
                productoDb.ProveedorId = producto.ProveedorId;
                productoDb.Estado = producto.Estado;
                _db.SaveChanges();
            }
        }
    }
}