using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nit Es Requerido")]
        [MaxLength(50)]
        [Display(Name = "Nit Proveedor")]
        public string Nit { get; set; }

        [Required(ErrorMessage = "El Nombre del Proveedor Es Requerido")]
        [MaxLength(50)]
        [Display(Name = "Nombre Proveedor")]
        public string NombreProveedor { get; set; }

        [Required(ErrorMessage = "La direccion del Proveedor Es Requerida")]
        [MaxLength(50)]
        [Display(Name = "Direccion Proveedor")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El Telefono del Proveedor Es Requerido")]
        [MaxLength(50)]
        [Display(Name = "Teléfono Proveedor")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La Ciudad del Proveedor Es Requerida")]
        [MaxLength(50)]
        [Display(Name = "Ciudad del Proveedor")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El Estado Del Proveedor Es Requerido")]
        public bool Estado { get; set; }
    }
}