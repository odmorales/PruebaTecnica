using Datos;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentacion.Models;

namespace libreriaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public readonly AuthorService? _authorService;
        public AuthorController(LibreriaContext context)
        {
            _authorService = new AuthorService(context);
        }
        // GET: api/<SincronizarController>
        [HttpGet]
        public IEnumerable<AuthorViewModel>? Get() => _authorService?.Consultar()!.
                Select(a => new AuthorViewModel(a));
       

        // GET api/<SincronizarController>/5
        [HttpGet("{id}")]
        public ActionResult<AuthorViewModel> Get(int id)
        {
            var author = _authorService?.ConsultarAuthor(id);

            if(author == null)
            {
                return NotFound("No se encontró ningún author registrado. ");
            }

            return Ok(new AuthorViewModel(author));
        }

        // POST api/<SincronizarController>
        [HttpPost]
        public ActionResult<List<AuthorViewModel>> Post(List<AuthorInputModel> authors)
        {

            List<Author> listauthors = Mapear(authors);


            var respuesta = _authorService!.Guardar(listauthors);

            if (respuesta.Error)
            {
                ModelState.AddModelError("Guardar autores", respuesta.Mensaje!);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
            }

            return Ok(respuesta.Objeto!.Select(r => new AuthorViewModel(r)));

        }

        [HttpDelete]
        public ActionResult<string> Delete()
        {
            string mensaje = _authorService!.EliminarTodos();

            return Ok(mensaje);
        }

        public List<Author> Mapear(List<AuthorInputModel> authors)
        {
            var lista = new List<Author>();

            foreach (var author in authors)
            {
                Author a = new Author();
                a.Id = author.Id;
                a.IdBook = author.IdBook;
                a.FirstName = author.FirstName;
                a.LastName = author.LastName;

                lista.Add(a);
            }

            return lista;
        }
    }
}
