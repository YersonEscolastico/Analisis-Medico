using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class AnalisisDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public string Resultado { get; set; }
        public string Analisis { get; set; }
        public AnalisisDetalle()
        {
            DetalleId = 0;
            Analisis = string.Empty;
            Resultado = string.Empty;
        }
        public AnalisisDetalle(string analisis, string resultado)
        {
            Analisis = analisis;
            Resultado = resultado;
        }
    }
}