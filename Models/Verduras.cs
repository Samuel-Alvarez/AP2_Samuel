using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcial2.Models
{
    public class Verduras
    {
        [Key]
        public int VerduraId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Nombre { get; set; }
        public string? Observaciones { get; set; }  
        
        [ForeignKey("VerduraId")]
        public List<VerdurasDetalle> Detalle { get; set; } = new List<VerdurasDetalle>();

    }

}