using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    public class TipoAnalisis
    {
        [Key]
        public int TiposId { get; set; }
        public string Analisis { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public TipoAnalisis()
        {
            TiposId = 0;
            Analisis = string.Empty;
            Precio = 0;
            Fecha = DateTime.Now;
        }
    }
}