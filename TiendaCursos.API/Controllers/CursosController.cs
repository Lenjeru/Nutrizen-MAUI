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
    [Route("/api/cursos")]
    public class CursosController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFicheroAlmacenamiento _almacenFicheros;
        private readonly string _contenedor;
        public CursosController(DataContext dataContext, IFicheroAlmacenamiento almacenFicheros)
        {
            _dataContext = dataContext;
            _almacenFicheros = almacenFicheros;
            _contenedor = "cursos";
        }

        [HttpPost]
        public async Task<ActionResult> Post(Curso curso)
        {
            try
            {
                if (!string.IsNullOrEmpty(curso.Imagen))
                {
                    var imagenCurso = Convert.FromBase64String(curso.Imagen);
                    curso.Imagen = await _almacenFicheros.GuardarFicheroAsync(imagenCurso, ".jpg", _contenedor);
                }

                _dataContext.Cursos.Add(curso);
                await _dataContext.SaveChangesAsync();
                return Ok(curso);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicada"))
                {
                    return BadRequest("Ya hay un curso con ese nombre");
                }

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dataContext.Cursos.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var curso = await _dataContext.Cursos.FirstOrDefaultAsync(x => x.Id == id);
            if (curso == null)
                return NotFound();

            return Ok(curso);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Curso curso)
        {
            try
            {
                if (!string.IsNullOrEmpty(curso.Imagen) &&
                     !curso.Imagen.Contains("https:"))
                {
                    var imagenCurso = Convert.FromBase64String(curso.Imagen);
                    curso.Imagen = await _almacenFicheros.GuardarFicheroAsync(imagenCurso, ".jpg", _contenedor);
                }

                _dataContext.Cursos.Update(curso);
                await _dataContext.SaveChangesAsync();
                return Ok(curso);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicada"))
                {
                    return BadRequest("Ya hay un curso con ese nombre");
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
            var curso = await _dataContext.Cursos.FirstOrDefaultAsync
                (x => x.Id == id);
            if (curso == null)
            return NotFound();

            _dataContext.Cursos.Remove(curso);
            await _dataContext.SaveChangesAsync();
            
           
            return Ok(curso);

        }



    }
}
