using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedControl.Entities
{
    public class Compromisso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompromissosId { get; set; }

        [ForeignKey("PacienterId")]
        public virtual Paciente Paciente { get; set; }

        [ForeignKey("ConsultaId")]
        public virtual Consulta Consulta { get; set; }


        public string Observacao { get; set; }
    }
}
