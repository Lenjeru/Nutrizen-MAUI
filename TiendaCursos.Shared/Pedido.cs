using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Shared
{
    public class Pedido
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Fecha { get; set; }

        public Usuario? Usuario { get; set; }
        public string? UsuarioId { get; set; }

        public EstadoPedidos estadoPedido { get; set; }

        public ICollection<PedidoDetalle> PedidoDetalles { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Display(Name = "Lineas Pedido")]
        public int Lines => PedidoDetalles == null ? 0 : PedidoDetalles.Count;

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        public decimal total => PedidoDetalles == null ? 0 : PedidoDetalles.Sum(x => x.platillo!.Precio);
    }
}

