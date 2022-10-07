using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class DetalleVentaRepositorio : Repositorio<DetalleVenta>, IDetalleVentaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public DetalleVentaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(DetalleVenta detalleVenta)
        {
            _db.Update(detalleVenta);
        }
    }
}