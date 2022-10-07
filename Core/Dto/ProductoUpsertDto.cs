using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Dto
{
    public class ProductoUpsertDto
    {

        public int Id { get; set; }

        public string NumeroSerie { get; set; }

        public string Descripcion { get; set; }

        public double Precio { get; set; }

        public double Costo { get; set; }

        public int Cantidad { get; set; }

        public string ImagenUrl { get; set; }

        public bool Estado { get; set; }
    }
}