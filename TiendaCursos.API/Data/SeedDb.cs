using TiendaCursos.API.Helpers;
using TiendaCursos.Shared;

namespace TiendaCursos.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsuarioHelper _userHelper;

        public SeedDb(DataContext context, IUsuarioHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await ValidarRolesAsync();
            await ValidarUsuarioAsync("Angel", "Beltran", "al439632@gmail.com", 
                "123456789",TipoUsuario.Administrador);
        }

        private async Task<Usuario> ValidarUsuarioAsync(string nombre, string apellido, string email,
         string telefono, TipoUsuario tipoUsuario)
        {
            var user = await _userHelper.DameUsuarioAsync(email);
            if (user == null)
            {
                user = new Usuario
                {
                    Nombre = nombre,
                    Apellidos = apellido,
                    Email = email,
                    UserName = email,
                    PhoneNumber = telefono,
                    tipoUsuario = tipoUsuario
                };

                await _userHelper.AgregarUsuarioAsync(user, "123456789");
                await _userHelper.AgregarUsuarioRolAsync(user, tipoUsuario.ToString());

            }

            return user;
        }

        private async Task ValidarRolesAsync()
        {
            await _userHelper.ValidarRolAsync(TipoUsuario.Administrador.ToString());
            await _userHelper.ValidarRolAsync(TipoUsuario.Usuario.ToString());
        }
    }
}
