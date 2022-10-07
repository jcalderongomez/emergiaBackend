using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class ProveedorUpsertDto
    {
        public int Id { get; set; }

        public string Nit { get; set; }

        public string NombreProveedor { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Ciudad { get; set; }

        public bool Estado { get; set; }
    }
}