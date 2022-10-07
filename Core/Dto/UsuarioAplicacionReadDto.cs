using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Dto
{
    public class UsuarioAplicacionReadDto
    {

        public string UserApp { get; set; }
        public string PasswordApp { get; set; }
        public string Rol { get; set; }
        public bool Estado { get; set; }

    }
}