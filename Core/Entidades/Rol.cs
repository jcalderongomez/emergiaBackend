using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El Nombre Del Rol Es Requerido")]
        [MaxLength(50)]
        [Display(Name = "Nombre del estado")]
        public string NombreRol { get; set; }
        
        [Required(ErrorMessage = "El Estado Del Es Requerido")]
        public bool Estado { get; set; }
    }
}