using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class VentaRepositorio : Repositorio<Venta>, IVentaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public VentaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Actualizar(Venta venta)
        {
            _db.Update(venta);
        }
    }
}
