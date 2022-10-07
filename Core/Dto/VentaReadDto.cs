using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Dto
{
    public class VentaReadDto
    {
        public int Id { get; set; }

        public string UsuarioAplicacion { get; set; }
        public DateTime FechaOrden { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string NumeroEnvio { get; set; }
        public string Carrier { get; set; }
        public double TotalOrden { get; set; }
        public string EstadoOrden { get; set; }
        public string EstadoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaMaximaPago { get; set; }
        public string Transaccion { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string NombresCliente { get; set; }
    }
}