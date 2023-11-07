using CursoEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Categoria> Categoria { get; set; }    
        public DbSet<Usuario> Usuario { get; set; }    
        public DbSet<Articulo> Articulo { get; set; }    
        public DbSet<DetalleUsuario> DetalleUsuario { get; set; }    
        public DbSet<Etiqueta> Etiqueta { get; set; }   
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticuloEtiqueta>().HasKey(ae => new
            {
                ae.Etiqueta_Id,
                ae.Articulo_Id
            });
        }
    }
}
