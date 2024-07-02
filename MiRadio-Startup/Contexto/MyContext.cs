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
        public DbSet<Etiqueta> Etiquetas { get; set; }     // DbSet para la entidad Etiqueta

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musica>()
                .HasMany(m => m.Etiquetas)      // Relación: Una Musica tiene muchas Etiquetas
                .WithOne(e => e.Musica)         // Relación: Cada Etiqueta pertenece a una Musica
                .HasForeignKey(e => e.MusicaId); // Clave foránea: MusicaId en la entidad Etiqueta

            base.OnModelCreating(modelBuilder);
        }
    }
}
