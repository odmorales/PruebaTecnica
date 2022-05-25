using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logica
{
    public class GuardarResponse<T>
    {
        public GuardarResponse(T objeto)
        {
            this.Error = false;
            this.Objeto = objeto;
        }
        public GuardarResponse(string mensaje)
        {
            this.Error = true;
            this.Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string? Mensaje { get; set; }
        public T? Objeto { get; set; }
    }
}