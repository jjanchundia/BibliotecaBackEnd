using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain
{
    public class Libro
    {
        [Key]
        public int LibroId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string Estado { get; set; }
        //public DateTime FechaCreacion { get; set; }
        // Especifica la relación con la entidad Users y la clave foránea UserId
        //[ForeignKey("UserId")]
        //public Users Users { get; set; }
        //public int UserId { get; set; }
    }
}
