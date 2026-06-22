using Microsoft.AspNetCore.Identity;
using TiendaCursos.Shared;

namespace TiendaCursos.API.Helpers
{
    public interface IUsuarioHelper
    {
        Task<Usuario> DameUsuarioAsync(string email);
        Task<IdentityResult> AgregarUsuarioAsync(Usuario usuario, string password);
        Task ValidarRolAsync(string rol);
        Task AgregarUsuarioRolAsync(Usuario usuario, string rol);
        Task<bool> IsUserInRoleAsync(Usuario usuario, string rol);
        Task<SignInResult> LoginAsync(LoginDTO model);
        Task LogoutAsync();
        Task<IdentityResult> CambioPasswordAsync(Usuario usuario, string passwordActual, string NuevaPassword);
        Task<IdentityResult> ActualizarUsuarioAsync(Usuario usuario);
    }
}
