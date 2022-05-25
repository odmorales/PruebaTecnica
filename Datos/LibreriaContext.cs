using Entidad;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class LibreriaContext: DbContext  
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options): base(options)
        {
            
        }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
    }
}