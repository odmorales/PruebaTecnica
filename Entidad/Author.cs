using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidad
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int? IdBook { get; set; }
        public Book? Book { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}