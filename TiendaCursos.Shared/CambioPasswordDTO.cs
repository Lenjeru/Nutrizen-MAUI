using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Shared
{
    public class CambioPasswordDTO
    {
        [DataType(DataType.Password)]
        [Display(Name = "Password actual")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PasswordActual { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Nueva Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PasswordNueva { get; set; } = null!;

        [Compare("PasswordNueva", ErrorMessage = "La nueva password y la confirmación no coinciden.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmación nueva password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Ok { get; set; } = null!;
    }
}
