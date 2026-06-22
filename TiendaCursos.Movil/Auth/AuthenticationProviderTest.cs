using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Movil.Auth
{
    public class AuthenticationProviderTest: AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(5000);
            //var anonimous = new ClaimsIdentity();
            //return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimous)));
            var usuarioJap = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "JAP"),
                new Claim("LastName", "SOFTWARE"),
                new Claim(ClaimTypes.Name, "japsoftware@gmail.com"),
                new Claim(ClaimTypes.Role,"Administrador")
                //new Claim(ClaimTypes.Role,"Usuario")
            }, authenticationType: "test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuarioJap)));

        }
    }
}
