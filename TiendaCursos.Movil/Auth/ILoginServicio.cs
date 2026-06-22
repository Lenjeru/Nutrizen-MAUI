using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Movil.Auth
{
    public interface ILoginServicio
    {
        Task LoginAsync(string token);
        Task LogoutAsync();

    }
}
