using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Dto
{
    public class UsuarioAplicacionUpsertDto
    {
        
        public string UserApp  { get; set; }
        public string PasswordApp { get; set; }
        public bool Estado { get; set; }
       
    }
}