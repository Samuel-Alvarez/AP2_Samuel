using Microsoft.EntityFrameworkCore;
using Parcial2.Models;

public class VitaminasBLL
    {
        private Contexto _contexto;
        public VitaminasBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

    
        public Vitaminas? Buscar(int Id)
    {
        return _contexto.Vitaminas
        .Where(p => p.VitaminaId == Id)
        .AsNoTracking()
        .SingleOrDefault();
    }
        public List<Vitaminas> GetList()
    {
         return _contexto.Vitaminas
        .AsNoTracking()
        .ToList();
     }
}