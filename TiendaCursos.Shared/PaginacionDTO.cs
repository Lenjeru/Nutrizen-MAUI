using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Shared
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        public int Registros { get; set; } = 8;
        public string? Filtros { get; set; }
    }
}
