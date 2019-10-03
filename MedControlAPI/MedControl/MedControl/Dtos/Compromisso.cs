using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedControl.Dtos
{
    public class Compromisso
    {
        public int CompromissosId { get; set; }
        public Paciente Paciente { get; set; }
        public Consulta Consulta { get; set; }
        public string Observacao { get; set; }
    }
}
