using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Shared
{
    public class PedidoDetalle
    {
        public int Id { get; set; }
        public Pedido? Pedido { get; set; }
        public int PedidoId { get; set; }
        public Platillo? platillo { get; set; }
        public int PlatilloId { get; set; }

    }
}
