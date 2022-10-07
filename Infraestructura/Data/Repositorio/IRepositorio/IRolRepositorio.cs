using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IRolRepositorio : IRepositorio<Rol>
    {
        void Actualizar(Rol rol);
    }
}
