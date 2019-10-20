using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class AnalisisDetalle
    {
        [Key]
        public int AnalisisDetalleId { get; set; }
        public string Resultado { get; set; }
        public int TiposId { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public AnalisisDetalle()
        {
            AnalisisDetalleId = 0;
            TiposId = 0;
            Resultado = string.Empty;
            Precio = 0;
            Fecha = DateTime.Now;
        }
        public AnalisisDetalle(int analisis, string resultado, DateTime date)
        {
            TiposId = analisis;
            Resultado = resultado;
            Fecha = date;
        }
    }
}