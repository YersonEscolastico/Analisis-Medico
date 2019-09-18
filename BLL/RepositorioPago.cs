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
                    contexto.Analisis.Find(pago.AnalisisId).Balance -= pago.MontoPago;

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

            try
            {
                Pagos PagoAnt = RepositorioPago.Buscar(pago.PagoId);


                decimal modificado = pago.MontoPago - PagoAnt.MontoPago;

                var Analisis = contexto.Analisis.Find(pago.AnalisisId);
                Analisis.Balance += modificado;
                RepositorioAnalisis.Modificar(Analisis);

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

                contexto.Analisis.Find(pago.AnalisisId).Balance += pago.MontoPago;

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


        public static Pagos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Pagos pago = new Pagos();

            try
            {
                pago = contexto.Pagos.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return pago;
        }


        public static List<Pagos> GetList(Expression<Func<Pagos, bool>> expression)
        {
            List<Pagos> pagos = new List<Pagos>();
            Contexto contexto = new Contexto();

            try
            {
                pagos = contexto.Pagos.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return pagos;
        }




    }
}