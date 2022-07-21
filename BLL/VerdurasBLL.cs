using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Parcial2.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class VerdurasBLL
    {
        private Contexto _contexto;

        public VerdurasBLL(Contexto contexto)
        {
            _contexto = contexto;
        }
        public bool Existe(int Id)
        {
            return _contexto.Verduras.Any(c => c.VerduraId == Id);
        }
        public bool Guardar(Verduras verdura)
        {
            bool paso = false;

            if (!Existe(verdura.VerduraId))
                paso = Insertar(verdura);
            else
                paso = Modificar(verdura);
               return paso;

        }
        private bool Insertar(Verduras verdura)
        {
            _contexto.Verduras.Add(verdura);
            
            foreach (var item in verdura.Detalle)
            {
                var vitamina = _contexto.Vitaminas.Find(item.VitaminaId);
                vitamina.Existencia += item.Cantidad;
            }
         
            bool insertar = _contexto.SaveChanges() >0;
            _contexto.Entry(verdura).State = EntityState.Detached;
            return insertar;           
        }
        
         private bool Modificar(Verduras verdura)
        {
            var anterior = _contexto.Verduras
           .Where(c => c.VerduraId == verdura.VerduraId)
           .Include(c => c.Detalle)
           .AsNoTracking()
           .SingleOrDefault();
 
            foreach (var item in anterior.Detalle)
            {
                var vitamina = _contexto.Vitaminas.Find(item.VitaminaId);

                vitamina.Existencia -= item.Cantidad;
            }        
            _contexto.Database.ExecuteSqlRaw($"DELETE FROM VerdurasDetalle WHERE VerduraId={verdura.VerduraId};");

            foreach (var item in verdura.Detalle)
            {
                var vitamina = _contexto.Vitaminas.Find(item.VitaminaId);
                vitamina.Existencia += item.Cantidad;
                

                _contexto.Entry(item).State = EntityState.Added;
            }

            _contexto.Entry(verdura).State = EntityState.Modified;

            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(verdura).State = EntityState.Detached;
            return guardo;
        }
       public bool Eliminar(Verduras verdura)
        {
              _contexto.Verduras.Add(verdura);
          
            foreach (var item in verdura.Detalle)
            {
                var vitamina = _contexto.Vitaminas.Find(item.VitaminaId);
                vitamina.Existencia -= item.Cantidad;
             
            }
            _contexto.Entry(verdura).State = EntityState.Deleted;

            bool elimino = _contexto.SaveChanges() > 0;
            _contexto.Entry(verdura).State = EntityState.Detached;
               
            return elimino;
        } 

        public Verduras? Buscar(int verdura)
        {
            return _contexto.Verduras
            .Include(c => c.Detalle)
            .Where(c => c.VerduraId == verdura)
            .AsNoTracking()
            .SingleOrDefault();
        }
        public List<Verduras> GetList()
        {
            return _contexto.Verduras.AsNoTracking().ToList();
        }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}