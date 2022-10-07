using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Dto
{
    public class DetalleVentaUpsertDto
    {
        public int Id { get; set; }
        public int Venta { get; set; }       
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
    }
}