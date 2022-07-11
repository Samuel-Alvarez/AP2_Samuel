  using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcial2.Models
{
   public class VerdurasDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public int VerduraId { get; set; }
        public int VitaminaId { get; set; }
        public double Cantidad { get; set; }


    }

    
}