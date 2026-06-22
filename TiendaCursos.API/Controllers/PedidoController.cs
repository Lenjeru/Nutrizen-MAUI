using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaCursos.API.Data;
using TiendaCursos.API.Helpers;
using TiendaCursos.Shared;

namespace TiendaCursos.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosHelper _pedidosHelper;
        private readonly DataContext _dataContext;
        private readonly IUsuarioHelper _usuarioHelper;
        public PedidosController(IPedidosHelper ordersHelper,
             DataContext dataContext, IUsuarioHelper usuarioHelper)
        {
            _pedidosHelper = ordersHelper;
            _dataContext = dataContext;
            _usuarioHelper = usuarioHelper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PedidoDTO pedidoDTO)
        {
            var response = await _pedidosHelper.ProcesarPedidoAsync(User.Identity!.Name!);
            if (response.ok)
            {
                return NoContent();
            }
            return BadRequest(response.Mensaje);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var usuario = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (usuario == null)
            {
                return BadRequest("Usuario no válido.");
            }

            var consultaSQL = _dataContext.Pedidos
                .Include(s => s.Usuario)
                .Include(s => s.PedidoDetalles)
                .ThenInclude(sd => sd.platillo).AsQueryable();

            var EsAdmin = await _usuarioHelper.IsUserInRoleAsync(usuario, TipoUsuario.Administrador.ToString());
            if (!EsAdmin)
                consultaSQL = consultaSQL.Where(s => s.Usuario!.Email == User.Identity!.Name);

            return Ok(await consultaSQL.OrderByDescending
                (x => x.Fecha).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var pedido = await _dataContext.Pedidos
              .Include(s => s.Usuario!)
              .Include(s => s.PedidoDetalles!)
              .ThenInclude(sd => sd.platillo)
              .FirstOrDefaultAsync(s => s.Id == id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        [HttpPut]
        public async Task<ActionResult> Put(PedidoDTO pedidoDTO)
        {
            var usuario = await _usuarioHelper.DameUsuarioAsync(User.Identity!.Name!);
            if (usuario == null)
            {
                return NotFound();
            }
            var EsAdmin = await _usuarioHelper.IsUserInRoleAsync(usuario, TipoUsuario.Administrador.ToString());
            if (!EsAdmin && pedidoDTO.Estado != EstadoPedidos.Cancelado)
                return BadRequest("Acceso restringido para administradores.");

            var pedido = await _dataContext.Pedidos
            .Include(s => s.PedidoDetalles)
            .ThenInclude(sd => sd.platillo)
            .FirstOrDefaultAsync(s => s.Id == pedidoDTO.Id);

            if (pedido == null)
            {
                return NotFound();
            }

            pedido.estadoPedido = pedidoDTO.Estado;
            _dataContext.Update(pedido);
            await _dataContext.SaveChangesAsync();

            return Ok(pedido);
        }
    }
}
