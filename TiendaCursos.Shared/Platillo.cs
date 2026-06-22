using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Shared
{
    public class Platillo
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Ingredientes")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(2000, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Ingredientes { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [Display(Name = "Calorias")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(2000, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Calorias { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(2000, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Categoria { get; set; } = null!;


        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Precio { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Cantidad { get; set; }

        [Display(Name = "Unidades")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Unidad { get; set; }


        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Imagen { get; set; } = null!;

        public ICollection<PedidoTemporal>? PedidoTemporal { get; set; }
        public ICollection<PedidoDetalle>? PedidoDetalles { get; set; }
    }
}

