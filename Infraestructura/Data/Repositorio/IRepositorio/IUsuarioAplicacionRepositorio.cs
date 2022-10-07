using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IUsuarioAplicacionRepositorio : IRepositorio<UsuarioAplicacion>
    {
        Task<int> Register(UsuarioAplicacion usuarioAplicacion, string password);
        Task<string> Login(string userName, string password);

        Task<bool> UserExiste(string userName);

        //void Actualizar(UsuarioAplicacion usuarioAplicacion);
    }
}