using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TiendaCursos.Movil.Helpers;

namespace TiendaCursos.Movil.Auth
{
    public class AuthenticationProviderJWT : AuthenticationStateProvider, ILoginServicio
    {
        private readonly IJSRuntime _jSRuntime;
        private readonly HttpClient _httpCliente;
        private readonly String _tokenClave;
        private readonly AuthenticationState _anonimo;
        public AuthenticationProviderJWT(IJSRuntime jSRuntime, 
            HttpClient httpClient)
        {
            _jSRuntime = jSRuntime;
            _httpCliente = httpClient;
            _tokenClave = "TOKEN_CLAVE";
            _anonimo = new AuthenticationState(new ClaimsPrincipal
    (new ClaimsIdentity()));
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jSRuntime.ObtenerLocalStorage(_tokenClave);
            if (token == null) 
            {
                return _anonimo;
            }

            return CrearEstadoAutenticacion(token.ToString()!);
        }

        public async Task LoginAsync(string token)
        {
            await _jSRuntime.GuardarLocalStorage(_tokenClave, token);
            var authState = CrearEstadoAutenticacion(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task LogoutAsync()
        {
            await _jSRuntime.BorrarLocalStorage(_tokenClave);
            _httpCliente.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(_anonimo));
        }

        private AuthenticationState CrearEstadoAutenticacion(string token)
        {
            _httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var claims = ParseClaimsJWT(token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsJWT(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var unserializedToken = jwtSecurityTokenHandler.ReadJwtToken(token);
            return unserializedToken.Claims;

        }
    }
}
