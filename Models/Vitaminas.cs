using System.ComponentModel.DataAnnotations;


namespace Parcial2.Models
{
    public class Vitaminas
    {
        [Key]
        public int VitaminaId { get; set; }
        public string? Descripcion { get; set; }
        public double UnidadDeMedida { get; set; }

    }
}