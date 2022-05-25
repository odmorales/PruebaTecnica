using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entidad;

namespace Presentacion.Models
{
    public class AuthorInputModel
    {
        [Required(ErrorMessage = "El id del autor es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El id del libro es requerido")]
        public int? IdBook { get; set; }

        [Required(ErrorMessage = "El name es requerido")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        public string? LastName { get; set; }
    }
    public class AuthorViewModel: AuthorInputModel
    {
        public AuthorViewModel(Author author)
        {
            this.Id = author.Id;
            this.IdBook = author.IdBook;
            this.Book = author.Book;
            this.FirstName = author.FirstName;
            this.LastName = author.LastName;
        }
        public Book? Book { get; set; }
    }
}