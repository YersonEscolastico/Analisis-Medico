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
        public DateTime FechaRegistro { get; set; }
        public TipoAnalisis()
        {
            TiposId = 0;
            Analisis = string.Empty;
            FechaRegistro = DateTime.Now;
        }
    }
}