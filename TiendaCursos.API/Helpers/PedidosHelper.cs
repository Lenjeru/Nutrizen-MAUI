using Microsoft.EntityFrameworkCore;
using TiendaCursos.API.Data;
using TiendaCursos.Shared;

namespace TiendaCursos.API.Helpers
{
    public class PedidosHelper : IPedidosHelper
    {
        private readonly DataContext _datacontext;
        public PedidosHelper(DataContext _context)
        {
            _datacontext = _context;
        }

        public async Task<Respuesta> ProcesarPedidoAsync(string email)
        {
            var usuario = await _datacontext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (usuario == null)
            {
                return new Respuesta
                {
                    ok = false,
                    Mensaje = "Usuario incorrecto"
                };
            }

            var pedidoTemporal = await _datacontext.PedidoTemporal
                .Include(x => x.Platillo)
                .Where(x => x.Usuario!.Email == email)
                .ToListAsync();

            Pedido pedido = new()
            {
                Fecha = DateTime.UtcNow,
                Usuario = usuario,
                PedidoDetalles = new List<PedidoDetalle>(),
                estadoPedido = EstadoPedidos.Nuevo
            };

            foreach (var pedTemp in pedidoTemporal)
            {
                pedido.PedidoDetalles.Add(new PedidoDetalle
                {
                    platillo = pedTemp.Platillo
                });

                _datacontext.PedidoTemporal.Remove(pedTemp);
            }

            _datacontext.Pedidos.Add(pedido);
            await _datacontext.SaveChangesAsync();
            Respuesta respuesta = new()
            {
                ok = true,
                Mensaje = "Pedido efectuado"
            };

            return respuesta;
        }
    }
}

