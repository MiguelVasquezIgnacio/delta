using Microsoft.EntityFrameworkCore;
using MiRadio_Startup.Models;

namespace MiRadio_Startup.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }       // DbSet para la entidad Usuario
        public DbSet<Musica> Musicas { get; set; }         // DbSet para la entidad Musica
        public DbSet<Comentario> Comentarios { get; set; } // DbSet para la entidad Comentario      
    }


}
