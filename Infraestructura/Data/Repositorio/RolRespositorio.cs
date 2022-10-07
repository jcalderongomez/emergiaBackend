using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class RolRespositorio : Repositorio<Rol>, IRolRepositorio
    {
        private readonly ApplicationDbContext _db;
        public RolRespositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Rol rol)
        {
            var rolDb = _db.Rol.FirstOrDefault(p => p.Id == rol.Id);
            if (rolDb != null)
            {
                rolDb.NombreRol = rol.NombreRol;
                rolDb.Estado = rol.Estado;
                _db.SaveChanges();
            }
        }
    }
}