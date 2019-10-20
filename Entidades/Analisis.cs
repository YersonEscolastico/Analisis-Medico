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
    public class Analisis
    {
        [Key]
        public int AnalisisId { get; set; }
        public string Paciente { get; set; }
        public decimal Balance { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public virtual List<AnalisisDetalle> Detalle { get; set; }

        public Analisis()
        {
            AnalisisId = 0;
            Paciente = string.Empty;
            Monto = 0;
            Balance = 0;
            Fecha = DateTime.Now;
            Detalle = new List<AnalisisDetalle>();
        }
    }
}