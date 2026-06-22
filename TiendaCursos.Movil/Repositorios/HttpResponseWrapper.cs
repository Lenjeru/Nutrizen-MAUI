using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TiendaCursos.Movil.Repositorios
{
    public class HttpResponseWrapper<T>
    {
        public bool Error { get; set; }
        public T? Response { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public HttpResponseWrapper(T? response, bool error, 
            HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }
            var codigoEstatus = HttpResponseMessage.StatusCode;
            if (codigoEstatus == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }
            else if (codigoEstatus == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if (codigoEstatus == HttpStatusCode.Unauthorized)
            {
                return "Tienes que iniciar sesión para hacer esta operación";
            }
            else if (codigoEstatus == HttpStatusCode.Forbidden)
            {
                return "No tienes permisos esta operación";
            }

            return "Ha ocurrido un error inesperado";
        }
    }
}
