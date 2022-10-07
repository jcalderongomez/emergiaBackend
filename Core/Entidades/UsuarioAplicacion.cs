using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class UsuarioAplicacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre Del Usuario de la Aplicación Es Requerido")]
        [MaxLength(50)]
        [Display(Name = "Nombre Usuario Aplicación")]
        public string UserApp { get; set; }

        [Required(ErrorMessage = "La Clave Del Usuario De La Aplicación Es Requerido")]
        [MaxLength(50)]
        [Display(Name = "Clave Usuario Aplicación")]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [Required]
        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Rol Rol { get; set; }

        [Required(ErrorMessage = "El Estado Del Usuario De La Aplicación Es Requerido")]
        public bool Estado { get; set; }

    }
}