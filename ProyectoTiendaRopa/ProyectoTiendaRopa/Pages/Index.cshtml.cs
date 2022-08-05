using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaRopa.datos;
using ProyectoTiendaRopa.modelos;

namespace ProyectoTiendaRopa.Pages
{
    public class IndexModel : PageModel
    {
        
        private readonly ApplicationDbContext _contexto;
        public IndexModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Producto> productos { set; get; }
        public async Task OnGet()
        {
            productos = await _contexto.producto.ToListAsync();
        }
    }
}