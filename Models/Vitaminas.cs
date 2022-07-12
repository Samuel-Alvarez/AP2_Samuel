using System.ComponentModel.DataAnnotations;


namespace Parcial2.Models
{
    public class Vitaminas
    {
        [Key]
        public int VitaminaId { get; set; }
        public string? Descripcion { get; set; }
        public double Existencia { get; set; }
        public string? UnidadMedida { get; set; }
      
        
    }
}