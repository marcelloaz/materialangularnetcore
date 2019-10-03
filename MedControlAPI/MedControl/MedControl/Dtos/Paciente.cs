using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedControl.Dtos
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
