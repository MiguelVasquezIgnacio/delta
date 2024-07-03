using Microsoft.EntityFrameworkCore;
using MiRadio_Startup.Models;

namespace MiRadio_Startup.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }       
        public DbSet<Musica> Musicas { get; set; }         
        public DbSet<Comentario> Comentarios { get; set; }       
    }


}