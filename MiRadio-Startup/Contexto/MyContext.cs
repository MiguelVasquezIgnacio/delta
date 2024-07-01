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
        public DbSet<Etiqueta> Etiquetas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musica>()
                .HasMany(m => m.Etiquetas)
                .WithOne(e => e.Musica)
                .HasForeignKey(e => e.MusicaId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
