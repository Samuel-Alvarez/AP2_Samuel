using Parcial2.Models;
using Microsoft.EntityFrameworkCore;

namespace Parcial2.BLL
{
    public class VerdurasBLL
    {
        private Contexto _contexto;
        public VerdurasBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public bool Existe(int id)
        {
            return _contexto.Verduras
                .Any(v => v.VerduraId == id);
        }

        public bool Guardar(Verduras verdura)
        {
            if (!Existe(verdura.VerduraId))
                return this.Insertar(verdura);
            else
                return this.Modificar(verdura);
        }


        private bool Insertar(Verduras verdura)
        {
            _contexto.Verduras.Add(verdura);

            //sumar 
            foreach (var item in verdura.Detalle)
            {
                var vitamina = _contexto.Vitaminas.Find(item.VitaminaId);
                vitamina.Existencia += item.Cantidad;
            }

            _contexto.Verduras.Add(verdura);

            return _contexto.SaveChanges() > 0;
        }

        private bool Modificar(Verduras verdura)
        {

            //buscar el detalle anterior
            var anterior = _contexto.Verduras
           .Where(c => c.VerduraId == verdura.VerduraId)
           .Include(c => c.Detalle)
           .AsNoTracking()
           .SingleOrDefault();


            //restar 
            foreach (var item in anterior.Detalle)
            {
                var vitamina = _contexto.Vitaminas.Find(item.VitaminaId);

                vitamina.Existencia -= item.Cantidad;
            }

            //borrar los items del detalle anterior
            _contexto.Database.ExecuteSqlRaw($"DELETE FROM ComprasDetalle WHERE CompraId={verdura.VerduraId};");

            //sumar
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
            _contexto.Entry(verdura).State = EntityState.Deleted;

            //sumar
            foreach (var item in verdura.Detalle)
            {
                var vitamina = _contexto.Vitaminas.Find(item.VitaminaId);
                vitamina.Existencia -= item.Cantidad;

            }

            return _contexto.SaveChanges() > 0;
        }

        public Verduras? Buscar(int verduraId)
        {
            return _contexto.Verduras
                .Include(c => c.Detalle)
                .Where(c => c.VerduraId == verduraId)
                .AsNoTracking()
                .SingleOrDefault();
        }
        public List<Verduras> GetList()
        {
            return _contexto.Verduras.AsNoTracking().ToList();
        }
    }
}