using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            public DateTime FechaRegistro { get; set; }
            public virtual List<AnalisisDetalle> Detalle { get; set; }

            public Analisis()
            {
                AnalisisId = 0;
                Paciente = string.Empty;
                FechaRegistro = DateTime.Now;
                Detalle = new List<AnalisisDetalle>();
            }
        }
    }