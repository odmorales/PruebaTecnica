using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entidad;


namespace Presentacion.Models
{
    public class BookInputModel
    {
        [Required(ErrorMessage = "El id es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El titulo es requerido")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int PageCount { get; set; }
        public string? PublishDate { get; set; }
    }

    public class BookViewModel: BookInputModel 
    {
        public BookViewModel(Book book)
        {
            this.Id = book.Id;
            this.Title = book.Title;
            this.Description = book.Description;
            this.PageCount = book.PageCount;
            this.PublishDate = book.PublishDate;
        }
    }
}