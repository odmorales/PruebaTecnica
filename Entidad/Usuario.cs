using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entidad
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Rol { get; set; }
    }
}