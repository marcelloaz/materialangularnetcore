using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedControl.Dtos
{
    public class Consulta
    {
        public int ConsultaId { get; set; }
        public DateTime DataHoraIncial { get; set; }
        public string HoraInicio { get; set; }
        public DateTime DataHoraFinal { get; set; }
        public string HoraFinal { get; set; }
    }
}
