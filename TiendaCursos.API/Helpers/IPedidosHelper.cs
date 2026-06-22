using TiendaCursos.Shared;

namespace TiendaCursos.API.Helpers
{
    public interface IPedidosHelper
    {
        Task<Respuesta> ProcesarPedidoAsync(string email);
    }
}
