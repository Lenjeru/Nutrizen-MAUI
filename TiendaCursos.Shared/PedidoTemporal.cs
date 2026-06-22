using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Shared
{
    public class PedidoTemporal
    {
        public int Id { get; set; }
        public Usuario? Usuario { get; set; }
        public string? UsuarioId { get; set; }
        public Platillo? Platillo { get; set; }
        public int PlatilloId { get; set; }
    }
}

