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
    [Route("/api/platillos")]
    public class PlatillosController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFicheroAlmacenamiento _almacenFicheros;
        private readonly string _contenedor;
        public PlatillosController(DataContext dataContext, IFicheroAlmacenamiento almacenFicheros)
        {
            _dataContext = dataContext;
            _almacenFicheros = almacenFicheros;
            _contenedor = "platillos";
        }

        [HttpPost]
        public async Task<ActionResult> Post(Platillo platillo)
        {
            try
            {
               /* if (!string.IsNullOrEmpty(platillo.Imagen))
                {
                    var imagenplatillo = Convert.FromBase64String(platillo.Imagen);
                    platillo.Imagen = await _almacenFicheros.GuardarFicheroAsync(imagenplatillo, ".jpg", _contenedor);
                }*/

                _dataContext.Platillos.Add(platillo);
                await _dataContext.SaveChangesAsync();
                return Ok(platillo);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicada"))
                {
                    return BadRequest("Ya hay un platillo con ese nombre");
                }

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("totalPaginas")]
        [AllowAnonymous]
        public async Task<ActionResult> DamePaginas([FromQuery] PaginacionDTO paginacion)
        {
            var query = _dataContext.Platillos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paginacion.Filtros))
            {
                query = query.Where(x => x.Nombre.ToLower().Contains(paginacion.Filtros.ToLower()));
            }

            double contador = await query.CountAsync();
            double totalPages = Math.Ceiling(contador / paginacion.Registros);
            return Ok(totalPages);
        }

        [HttpGet("{id:int}")]
        
        public async Task<IActionResult> Get(int id)
        {
            var platillo = await _dataContext.Platillos.FirstOrDefaultAsync(x => x.Id == id);
            if (platillo == null)
                return NotFound();

            return Ok(platillo);
        }

        [HttpGet("PlatillosUsuario")]
        public async Task<IActionResult> PlatillosUsuario([FromQuery] PaginacionDTO paginacion)
        {
            var PlatillosNoComprados = await _dataContext.Platillos
           .Where(c => !_dataContext.Pedidos
           .Any(p => p.Usuario!.Email == User.Identity!.Name &&
                     p.PedidoDetalles.Any(pd => pd.PlatilloId == c.Id)))
            .Where(x => string.IsNullOrWhiteSpace(paginacion.Filtros)
            || x.Nombre.ToLower().Contains(paginacion.Filtros.ToLower()))
            .Paginar(paginacion)
            .ToListAsync();

            return Ok(PlatillosNoComprados);
        }

        [HttpGet("PlatillosUsuarioPaginas")]
        public async Task<ActionResult> PlatillosUsuarioPaginas([FromQuery] PaginacionDTO paginacion)
        {
            var query = _dataContext.Platillos
            .Where(c => !_dataContext.Pedidos
            .Any(p => p.Usuario!.Email == User.Identity!.Name &&
                     p.PedidoDetalles.Any(pd => pd.PlatilloId == c.Id)))
            .Where(x => string.IsNullOrWhiteSpace(paginacion.Filtros)
            || x.Nombre.ToLower().Contains(paginacion.Filtros.ToLower()))
            .AsQueryable();

            double contador = await query.CountAsync();
            double totalPages = Math.Ceiling(contador / paginacion.Registros);
            return Ok(totalPages);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Platillo platillo)
        {
            try
            {
                if (!string.IsNullOrEmpty(platillo.Imagen) &&
                     !platillo.Imagen.Contains("https:"))
                {
                    /*  var imagenplatillo = Convert.FromBase64String(platillo.Imagen);
                      platillo.Imagen = await _almacenFicheros.GuardarFicheroAsync(imagenplatillo, ".jpg", _contenedor);*/
                }

                _dataContext.Platillos.Update(platillo);
                await _dataContext.SaveChangesAsync();
                return Ok(platillo);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicada"))
                {
                    return BadRequest("Ya hay un platillo con ese nombre");
                }

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var platillo  = await _dataContext.Platillos.FirstOrDefaultAsync
                (x => x.Id == id);
            if (platillo == null)
                return NotFound();

            _dataContext.Platillos.Remove(platillo);
            await _dataContext.SaveChangesAsync();


            return Ok(platillo);

        }



    }
}
