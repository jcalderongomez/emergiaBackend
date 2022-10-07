using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Numero de Serie Es Requerido")]
        [MaxLength(30)]
        [Display(Name = "Numero de Serie")]
        public string NumeroSerie { get; set; }

        [Required(ErrorMessage = "La Descripción Es Requerida")]
        [MaxLength(60)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Precio Es Requerido")]
        [Range(1, 100000)]
        [Display(Name = "Precio")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "El Costo Es Requerido")]
        [Range(1, 100000)]
        [Display(Name = "Costo")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Costo { get; set; }

        [Required(ErrorMessage = "La Cantidad Es Requerida")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        public string ImagenUrl { get; set; }
        
        public int ProveedorId { get; set; }

        [ForeignKey("ProveedorId")]
        public Proveedor Proveedor { get; set; }

        [Required(ErrorMessage = "El Estado Es Requerido")]
        public bool Estado { get; set; }
    }
}