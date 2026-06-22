using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaCursos.API.Data;
using TiendaCursos.Shared;

namespace TiendaCursos.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/PedidoTemporal")]
    public class PedidoTemporalController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public PedidoTemporalController(DataContext context)
        {
            _dataContext = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PedidoTemporalDTO pedidoTemporalDTO)
        {
            Platillo platillo = await _dataContext.Platillos.FirstOrDefaultAsync(x => x.Id == pedidoTemporalDTO.PlatilloId);
            if (platillo == null)
            {
                return NotFound();
            }

            var usuario = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (usuario == null)
            {
                return NotFound();
            }

            var PedidoTemporal = new PedidoTemporal
            {
                Platillo = platillo,
                Usuario = usuario
            };

            try
            {
                _dataContext.Add(PedidoTemporal);
                await _dataContext.SaveChangesAsync();
                return Ok(pedidoTemporalDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _dataContext.PedidoTemporal
                 .Include(ts => ts.Usuario!)
                 .Include(ts => ts.Platillo!)
                 .Where(x => x.Usuario!.Email == User.Identity!.Name)
                 .ToListAsync());
        }

        [HttpGet("contador")]
        public async Task<ActionResult> GetCount()
        {
            return Ok(await _dataContext.PedidoTemporal
            .Where(x => x.Usuario!.Email == User.Identity!.Name)
            .SumAsync(x => 1));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _dataContext.PedidoTemporal
             .Include(ts => ts.Usuario!)
             .Include(ts => ts.Platillo!)
             .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> BorrarAsync(int id)
        {
            var pedidoTemporal = await _dataContext.PedidoTemporal.FirstOrDefaultAsync(x => x.Id == id);
            if (pedidoTemporal == null)
            {
                return NotFound();
            }
            _dataContext.Remove(pedidoTemporal);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }

}
