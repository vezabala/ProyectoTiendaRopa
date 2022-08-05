using Microsoft.EntityFrameworkCore;
using ProyectoTiendaRopa.modelos;

namespace ProyectoTiendaRopa.datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        {

        }
        // colocar modelos de tablas 
        public DbSet<Producto> producto { get; set; }
    }
}
