using System.ComponentModel.DataAnnotations;

namespace ProyectoTiendaRopa.modelos
{
    public class Producto
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Nombre producto")]
        public string? nombreProducto { get; set; }

        [Display(Name = "Descripcion")]
        public string? descripcion { get; set; }

        [Display(Name = "Precio")]
        public int precio { get; set; }

        [Display(Name = "Imagen (Solo se acepta extencion .png)")]
        public string? imagen { get; set; }
    }
}
