using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TiendaCursos.API.Helpers;
using TiendaCursos.Shared;

namespace TiendaCursos.API.Controllers
{
    [ApiController]
    [Route("/api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioHelper _usuarioHelper;
        private readonly IConfiguration _configuration;
        public UsuariosController(IUsuarioHelper userHelper, 
            IConfiguration configuration)
        {
            _usuarioHelper = userHelper;
            _configuration = configuration;
        }

        [HttpPost("CrearUsuario")]
        public async Task<ActionResult> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            Usuario usuario = usuarioDTO;
            var result = await _usuarioHelper.AgregarUsuarioAsync(usuario,
                usuarioDTO.Password);
            if (result.Succeeded)
            {
                await _usuarioHelper.AgregarUsuarioRolAsync(usuario, usuario.tipoUsuario.ToString()); 
                return Ok(BuildToken(usuario));
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _usuarioHelper.LoginAsync(loginDTO);
            if (result.Succeeded)
            {
                var user = await _usuarioHelper.DameUsuarioAsync(loginDTO.Email);
                return Ok(BuildToken(user));
            }
            return BadRequest("Email o contraseña incorrectos.");
        }
        
        [HttpPost("CambioPassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> CambioPassAsync(CambioPasswordDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _usuarioHelper.DameUsuarioAsync(User.Identity!.Name!);
            
            if (usuario == null)
                return NotFound();

            var result = await _usuarioHelper.CambioPasswordAsync(usuario, DTO.PasswordActual, DTO.PasswordNueva);

            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault().Description);

            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(Usuario usuario)
        {
            try
            {
                var usuarioActual = await _usuarioHelper.DameUsuarioAsync(usuario.Email!);
                if (usuarioActual == null)
                    return NotFound();

                usuarioActual.Nombre = usuario.Nombre;
                usuarioActual.Apellidos = usuario.Apellidos;
                usuarioActual.PhoneNumber = usuario.PhoneNumber;
                var result = await _usuarioHelper.ActualizarUsuarioAsync(usuarioActual);
                if (result.Succeeded)
                {
                    return NoContent();
                }

                return BadRequest(result.Errors.FirstOrDefault());

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); 
            }        
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Get()
        {
            return Ok( await _usuarioHelper.DameUsuarioAsync(User.Identity!.Name));
        }
        private TokenDTO BuildToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, usuario.Email!),
                 new Claim(ClaimTypes.Role, usuario.tipoUsuario.ToString()),
                 new Claim("Nombre", usuario.Nombre),
                 new Claim("Apellido", usuario.Apellidos),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:ClaveSecreta"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(15);
            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
