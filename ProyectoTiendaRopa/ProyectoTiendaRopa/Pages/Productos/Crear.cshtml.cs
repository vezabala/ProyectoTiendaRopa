using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using ProyectoTiendaRopa.datos;
using ProyectoTiendaRopa.modelos;

namespace ProyectoTiendaRopa.Pages.Productos
{
    public class CrearModel : PageModel
    {

        private readonly ApplicationDbContext _contexto;

        [BindProperty]
        public SubirArchivoModel subirArchivo { get; set; }

        [TempData]
        public string mensaje { get; set; }
        public CrearModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [BindProperty]
        public Producto productos { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //validar extension png
            string getArchivo = subirArchivo.imagen.FileName;
            string extencionValidacion = Strings.Right(getArchivo, 3);
            if (extencionValidacion.Equals("png") || extencionValidacion.Equals("jpg") || extencionValidacion.Equals("gif"))
            {

                string nombreArchivo = $"2.{extencionValidacion}";
                string directorio = Directory.GetCurrentDirectory() + "\\wwwroot\\img";
                string validacionDirectorio = directorio + "\\" + nombreArchivo;
                //Condicion para guardar imagen en directorio y nombre en la base de datos
                //Chequeo que la extensión sea efectivamente de una imagen
                if (System.IO.Path.GetExtension(validacionDirectorio).ToLower() == ".png" ||System.IO.Path.GetExtension(validacionDirectorio).ToLower() == ".jpg" || System.IO.Path.GetExtension(validacionDirectorio).ToLower() == ".gif")
                {
                    bool condicion = true;
                    while (condicion == true)
                    {
                        //Dado el caso, verifico que exista el archivo..
                        if (System.IO.File.Exists(validacionDirectorio))
                        {

                            string variableNombrePng = nombreArchivo[..^4];
                            int numero = int.Parse(variableNombrePng) + 1;
                            nombreArchivo = $"{numero}.{extencionValidacion}";
                            validacionDirectorio = directorio + "\\" + nombreArchivo;

                        }
                        else
                        {
                            string path = Path.Combine(directorio, nombreArchivo);
                            using var stream = new FileStream(path, FileMode.Create);
                            await subirArchivo.imagen.CopyToAsync(stream);
                            productos.imagen = nombreArchivo;
                            condicion = false;
                        }
                    }
                }


            }
            else
            {
                mensaje = "Error la extension de la imagen No corresponde";
                return Page();
            }


            _contexto.Add(productos);
            await _contexto.SaveChangesAsync();
            mensaje = "Registro se ha guardado correctamente";
            return RedirectToPage("Index");
        }
    }
    public class SubirArchivoModel
    {
        public IFormFile imagen { get; set; }
    }

}

