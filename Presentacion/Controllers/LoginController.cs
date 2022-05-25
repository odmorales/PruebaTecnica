using Datos;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentacion.Config;
using Presentacion.Models;
using Presentacion.Service;
using System.Text.Json;

namespace libreriaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private UsuarioService usuarioService;
        JwtService _jwtService;
        public LoginController(LibreriaContext context, IOptions<AppSetting> appSettings)
        {
            usuarioService = new UsuarioService(context);
            _jwtService = new JwtService(appSettings);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<LoginViewModel> Post(Usuario usuario)
        {  
            var respuesta = _jwtService.GenerarToken(usuario);

            return Ok(respuesta);
        }

        [HttpGet("RenovarToken")]
        public async Task<ActionResult<LoginViewModel>> RenovarToken()
        {
            string token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;

            var nombreUsuario = _jwtService.VerificarToken(token);

            if (nombreUsuario == null)
                return Unauthorized("Token no válido 1");

            Usuario usuario = new Usuario();
            var url = "https://fakerestapi.azurewebsites.net/api/v1/Users";
            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true};

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonSerializer.Deserialize<List<Usuario>>(content, options);

                    foreach (var user in users!)
                    {
                        if(user.UserName == nombreUsuario)
                        {
                            usuario = user;
                        }
                    }
                }
            }

            if (usuario == null)
                return Unauthorized("Token no válido 2");

            var respuesta = _jwtService.GenerarToken(usuario);

            return Ok(respuesta);
        }
    }
}
