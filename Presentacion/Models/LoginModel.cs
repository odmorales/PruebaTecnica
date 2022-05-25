using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "El usuarioes requerido")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string? Password { get; set; }
    } 

    public class LoginViewModel: LoginInputModel
    {
        public string? Rol { get; set; }
        public string? Token { get; set; }
    }
}