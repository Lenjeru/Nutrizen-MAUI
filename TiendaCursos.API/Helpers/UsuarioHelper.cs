using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TiendaCursos.API.Data;
using TiendaCursos.Shared;

namespace TiendaCursos.API.Helpers
{
    public class UsuarioHelper : IUsuarioHelper
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Usuario> _gestionUsuario;
        private readonly RoleManager<IdentityRole> _gestionRoles;
        private readonly SignInManager<Usuario> _gestionSesiones;

        public UsuarioHelper(DataContext dataContext, UserManager<Usuario>
             gestionUsuario, RoleManager<IdentityRole> gestionRoles
             , SignInManager<Usuario> gestionSesiones)
        {
            _dataContext = dataContext;
            _gestionUsuario = gestionUsuario;
            _gestionRoles = gestionRoles;
            _gestionSesiones = gestionSesiones;
        }

        public async Task<IdentityResult> AgregarUsuarioAsync(Usuario usuario, 
            string password)
        {
            return await _gestionUsuario.CreateAsync(usuario,password);
        }

        public async Task AgregarUsuarioRolAsync(Usuario usuario, string rol)
        {
            await _gestionUsuario.AddToRoleAsync(usuario, rol);
        }

        public async Task<IdentityResult> CambioPasswordAsync(Usuario usuario, string passwordActual, string NuevaPassword)
        {
            return await _gestionUsuario.ChangePasswordAsync(usuario, passwordActual, NuevaPassword);
        }

        public async Task<Usuario> DameUsuarioAsync(string email)
        {
           return await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> IsUserInRoleAsync(Usuario usuario, string rol)
        {
            return await _gestionUsuario.IsInRoleAsync(usuario, rol);
        }

        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
           return await _gestionSesiones.PasswordSignInAsync(model.Email, model.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await _gestionSesiones.SignOutAsync();
        }

        public async Task ValidarRolAsync(string rol)
        {
            bool existeRol = await _gestionRoles.RoleExistsAsync(rol);
            if (!existeRol)
            {
                await _gestionRoles.CreateAsync(new IdentityRole
                {
                    Name = rol
                });
            }
        }

        public async Task<IdentityResult> ActualizarUsuarioAsync(Usuario usuario)
        {
            return await  _gestionUsuario.UpdateAsync(usuario);
        }
    }
}
