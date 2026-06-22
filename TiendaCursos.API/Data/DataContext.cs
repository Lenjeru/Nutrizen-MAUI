using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TiendaCursos.Shared;

namespace TiendaCursos.API.Data
{
    public class DataContext:IdentityDbContext<Usuario>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Curso> Cursos { get; set; }

       
        public DbSet<Platillo> Platillos { get; set; }
        public DbSet<PedidoTemporal> PedidoTemporal { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalle> pedidoDetalles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Curso>().HasIndex(c => c.Nombre).IsUnique();
            modelBuilder.Entity<Platillo>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
