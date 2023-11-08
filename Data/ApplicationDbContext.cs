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
        public DbSet<ArticuloEtiqueta> ArticuloEtiqueta { get; set; }   
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API Categoria
            modelBuilder.Entity<Categoria>().HasKey(c => c.Categoria_Id);
            modelBuilder.Entity<Categoria>().Property(c => c.Nombre).IsRequired();
            modelBuilder.Entity<Categoria>().Property(c => c.FechaCreacion).HasColumnType("date");

            //Fluent API Articulo
            modelBuilder.Entity<Articulo>().HasKey(a => a.Articulo_Id);
            modelBuilder.Entity<Articulo>().Property(a => a.TituloArticulo).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Articulo>().Property(a => a.Descripcion).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Articulo>().Property(a => a.Fecha).HasColumnType("date");

            modelBuilder.Entity<Articulo>().ToTable("Tbl_Articulo");
            modelBuilder.Entity<Articulo>().Property(a => a.TituloArticulo).HasColumnName("Titulo");

            //Fluent API Usuario
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            //Not Mapped
            modelBuilder.Entity<Usuario>().Ignore(u => u.Edad);

            //Fluent API DetalleUsuario
            modelBuilder.Entity<DetalleUsuario>().HasKey(d => d.DetalleUsuario_Id);
            modelBuilder.Entity<DetalleUsuario>().Property(d => d.Cedula).IsRequired();

            //Fluent API Etiqueta
            modelBuilder.Entity<Etiqueta>().HasKey(e => e.Etiqueta_Id);
            modelBuilder.Entity<Etiqueta>().Property(e => e.Fecha).HasColumnType("date");

            //Relation One To One
            modelBuilder.Entity<Usuario>()
                .HasOne(c => c.DetalleUsuario)
                .WithOne(c => c.Usuario).HasForeignKey<Usuario>("DetalleUsuario_Id");

            //Relation One To Many --> Categoria y Articulo
            modelBuilder.Entity<Articulo>()
                .HasOne(c => c.Categoria)
                .WithMany(c => c.Articulo).HasForeignKey(c => c.Categoria_Id);

            //Relation Many To Many --> Articulo y Etiqueta
            modelBuilder.Entity<ArticuloEtiqueta>().HasKey(ae => new { ae.Etiqueta_Id, ae.Articulo_Id });
            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasOne(a => a.Articulo)
                .WithMany(a => a.ArticuloEtiqueta).HasForeignKey(c => c.Articulo_Id);
            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasOne(a => a.Etiqueta)
                .WithMany(a => a.ArticuloEtiqueta).HasForeignKey(c => c.Etiqueta_Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
