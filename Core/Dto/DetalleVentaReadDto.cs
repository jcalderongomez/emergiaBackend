using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Dto
{
    public class DetalleVentaReadDto
    {
        public int Id { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public double Precio { get; set; }
    }
}