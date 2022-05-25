using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datos;
using Entidad;

namespace Logica
{
    public class UsuarioService
    {
        private readonly LibreriaContext _context;
        public UsuarioService(LibreriaContext context) => _context = context;
        public Usuario? ValidarCredenciales(string nombreUsuario, string contrasena) => _context.Usuarios?
            .FirstOrDefault(t => t.UserName == nombreUsuario.ToUpper() && t.Password == contrasena);

        public Usuario? ConsulterUsuario(string nombreUsuario) => _context.Usuarios?
            .FirstOrDefault(t => t.UserName == nombreUsuario);
        
    }
}