using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class DetalleVenta
    {
        [Key]
        public int Id { get; set; }

        public int VentaId { get; set; }

        [ForeignKey("VentaId")]
        public Venta Ventas { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        public double Precio { get; set; }
    }
}