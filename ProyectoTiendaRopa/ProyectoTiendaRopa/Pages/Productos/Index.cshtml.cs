using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoTiendaRopa.datos;
using ProyectoTiendaRopa.modelos;

namespace ProyectoTiendaRopa.Pages.Productos
{
    public class IndexModel : PageModel
    {


        private readonly ApplicationDbContext _contexto;
        [TempData]
        public string mensaje { get; set; }

        public IndexModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public IEnumerable<Producto> productos { set; get; }
        public async Task OnGet()
        {
            productos = await _contexto.producto.ToListAsync();
        }

        public async Task<IActionResult> OnPostBorrar(int id, String imagen)
        {
            //borrar imagen del directorio
            string directorio = Directory.GetCurrentDirectory() + "\\wwwroot\\img";
            string validacionDirectorio = directorio + "\\" + imagen;

            if (System.IO.Path.GetExtension(validacionDirectorio).ToLower() == ".png" || System.IO.Path.GetExtension(validacionDirectorio).ToLower() == ".jpg" || System.IO.Path.GetExtension(validacionDirectorio).ToLower() == ".gif")
            {
                //Dado el caso, verifico que exista el archivo..
                if (System.IO.File.Exists(validacionDirectorio))
                {
                    System.IO.File.Delete(validacionDirectorio);
                }
            }

            var Productobd = await _contexto.producto.FindAsync(id);
            _contexto.producto.Remove(Productobd);
            await _contexto.SaveChangesAsync();
            mensaje = "Registro se ha eliminado correctamente";
            return RedirectToPage("Index");
        }

    }
}
