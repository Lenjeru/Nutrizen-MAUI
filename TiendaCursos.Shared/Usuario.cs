using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Shared
{
    public class Usuario : IdentityUser
    {
       

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Apellidos { get; set; } = null!;

        [Display(Name = "Tipo de usuario")]
        public TipoUsuario tipoUsuario { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellidos}";
        public ICollection<PedidoTemporal>? PedidoTemporal { get; set; }
    }

}
