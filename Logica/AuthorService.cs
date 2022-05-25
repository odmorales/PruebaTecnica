using Datos;
using Entidad;
using Microsoft.EntityFrameworkCore;

namespace Logica
{
    public class AuthorService
    {
        private readonly LibreriaContext? _context;
        private BookService _bookService;
        
        public AuthorService(LibreriaContext context)
        {
            _context = context;
            _bookService = new BookService(_context);
        }

        public string EliminarTodos()
        {
            try
            {
                var authors = _context?.Authors?.ToList();

                if (authors?.Count != 0)
                {
                    _context?.Authors?.RemoveRange(authors!);
                    _context?.SaveChanges();

                    return "Registro eliminado";
                }
                else
                {
                    return "Registro vacio";
                }
            }
            catch (System.Exception)
            {
                return "Error";
            }
        }
        public IEnumerable<Author>? Consultar() => _context?.Authors?
            .Include("Book").ToList();

        public Author? ConsultarAuthor(int id) => _context?.Authors?
            .Where(a => a.Id == id)
            .FirstOrDefault();

        public GuardarResponse<List<Author>> Guardar(List<Author> authors)
        {

            try
            {
                var listAuthors = _context?.Authors?.ToList();

                if (listAuthors?.Count != 0)
                {
                    EliminarTodos();
                }
                _context?.Database.OpenConnection();
                try
                {
                    _context?.Authors?.FromSqlRaw("EXECUTE SET IDENTITY_INSERT Authors ON");
                    _context?.Authors?.AddRange(authors);
                    _context?.SaveChanges();
                    _context?.Authors?.FromSqlRaw("EXECUTE SET IDENTITY_INSERT Authors OFF");
                                    }
                finally
                {
                    _context?.Database.CloseConnection();
                }
                
                return new GuardarResponse<List<Author>>(authors);
            }
            catch (System.Exception)
            {
                
                return new GuardarResponse<List<Author>>("Error al guardar los autores");
            }

        }
    }
}