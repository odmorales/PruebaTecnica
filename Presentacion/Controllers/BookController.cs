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
    public class BookController : ControllerBase
    {
        public readonly BookService? _bookService;

        public BookController(LibreriaContext context)
        {
            _bookService = new BookService(context);
        }

        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<BookViewModel>? Get() => _bookService?.Consultar()!.
                Select(a => new BookViewModel(a));

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public ActionResult<BookViewModel> Get(int id)
        {
            var book = _bookService?.ConsultarBook(id);

            if (book == null)
            {
                return NotFound("No se encontró ningún book registrado. ");
            }

            return Ok(new BookViewModel(book));
        }

        // POST api/<BookController>
        [HttpPost]
        public ActionResult<List<BookViewModel>> Post(List<BookInputModel> books)
        {

            List<Book> listaBooks = Mapear(books);


            var respuesta = _bookService!.Guardar(listaBooks);

            if (respuesta.Error)
            {
                ModelState.AddModelError("Guardar libros", respuesta.Mensaje!);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
            }

            return Ok(respuesta.Objeto!.Select(r => new BookViewModel(r)));

        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete]
        public ActionResult<string> Delete()
        {
            string mensaje = _bookService!.EliminarTodos();

            return Ok(mensaje);
        }

        public List<Book> Mapear(List<BookInputModel> books)
        {
            var lista = new List<Book>();

            foreach (var book in books)
            {
                Book b = new Book();
                b.Id = book.Id;
                b.Title = book.Title;
                b.Description = book.Description;
                b.PageCount = book.PageCount;
                b.PublishDate = book.PublishDate;

                lista.Add(b);
            }

            return lista;
        }
    }
}
