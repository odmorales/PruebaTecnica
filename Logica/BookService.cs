using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datos;
using Entidad;
using Microsoft.EntityFrameworkCore;

namespace Logica
{
    public class BookService
    {
        private readonly LibreriaContext? _context;

        public BookService(LibreriaContext context)
        {
            _context = context;
        }

        public string EliminarTodos()
        {
            try
            {
                var books = _context?.Books?.ToList();

                if (books?.Count != 0)
                {
                    _context?.Books?.RemoveRange(books!);
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

        public IEnumerable<Book>? Consultar() => _context?.Books?.ToList();

        public Book? ConsultarBook(int id) => _context?.Books?
            .Where(a => a.Id == id)
            .FirstOrDefault();

        public GuardarResponse<List<Book>> Guardar(List<Book> books)
        {

            try
            {
                var listBooks = _context?.Books?.ToList();

                if(listBooks?.Count != 0)
                {
                    EliminarTodos();
                }
                _context?.Database.OpenConnection();
                try
                {
                    _context?.Books?.FromSqlRaw("EXECUTE SET IDENTITY_INSERT Books ON");
                    _context?.Books?.AddRange(books);
                    _context?.SaveChanges();
                    _context?.Books?.FromSqlRaw("EXECUTE SET IDENTITY_INSERT Books OFF");

                }
                finally
                {
                    _context?.Database.CloseConnection();
                }

                return new GuardarResponse<List<Book>>(books);
            }
            catch (System.Exception)
            {

                return new GuardarResponse<List<Book>>("Error al guardar los libros");
            }

        }
    }
}