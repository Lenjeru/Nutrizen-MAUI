using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TiendaCursos.Movil.Helpers
{
    public static class IJSRuntimeExtension
    {
        public static ValueTask<object> GuardarLocalStorage(this IJSRuntime js,
            string clave, string valor)
        { 
          return  js.InvokeAsync<object>("localStorage.setItem", clave,valor);
        }

        public static ValueTask<object> ObtenerLocalStorage(this IJSRuntime js,
             string clave)
        {
            return js.InvokeAsync<object>("localStorage.getItem", clave);
        }

        public static ValueTask<object> BorrarLocalStorage(this IJSRuntime js,
       string clave)
        {
            return js.InvokeAsync<object>("localStorage.removeItem", clave);
        }
    }
}
