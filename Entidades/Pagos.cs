using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pagos
    {
        [Key]
        public int PagoId { get; set; }
        public int AnalisisId { get; set; }
        public decimal MontoPago { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Pagos()
        {
            PagoId = 0;
            AnalisisId = 0;
            FechaRegistro = DateTime.Now;
            MontoPago = 0;
        }
    }
}