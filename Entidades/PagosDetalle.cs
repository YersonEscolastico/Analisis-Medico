using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PagosDetalle
    {
        public int DetallePagoId { get; set; }
        public int PagoId { get; set; }
        public int AnalisisId { get; set; }
        public decimal MontoPago { get; set; }

        public DateTime FechaRegistro { get; set; }
        public PagosDetalle()
        {
            DetallePagoId = 0;
            PagoId = 0;
            AnalisisId = 0;
            MontoPago = 0;
            FechaRegistro = DateTime.Now;
        }
        public PagosDetalle( int analisisId, decimal montoPago)
        {
            AnalisisId = analisisId;
            MontoPago = montoPago;
            FechaRegistro = DateTime.Now;
        }
    }
}