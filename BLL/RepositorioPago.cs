using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entidades;


namespace BLL
{
    public class RepositorioPago
    {
        public static bool Guardar(Pagos pago)
        {

            bool paso = false;

            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Pagos.Add(pago) != null)
                {
                    foreach (var item in pago.Detalle)
                    {
                        contexto.Analisis.Find(item.AnalisisId).Balance -= (decimal)item.MontoPagado;
                    }

                    //contexto.Analisis.Find(pago.AnalisisId).Balance -= (decimal)pago.Pagado;

                    contexto.SaveChanges(); 
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public static bool Modificar(Pagos pago)
        {
            bool paso = false;

            Contexto contexto = new Contexto();
            RepositorioBase<Pagos> r = new RepositorioBase<Pagos>();
            RepositorioBase<Analisis> a = new RepositorioBase<Analisis>();
            try
            {
                Pagos PagoAnt = r.Buscar(pago.PagosId);


                decimal modificado = pago.Pagado - PagoAnt.Pagado;

                var Analisis = contexto.Analisis.Find(pago.AnalisisId);
                Analisis.Balance += modificado;
                a.Modificar(Analisis);

                contexto.Entry(pago).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;

            Contexto contexto = new Contexto();
            try
            {
                Pagos pago = contexto.Pagos.Find(id);

                contexto.Analisis.Find(pago.AnalisisId).Balance += pago.Pagado;

                contexto.Pagos.Remove(pago);

                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

    }
}