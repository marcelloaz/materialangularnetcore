using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedControl.Entities
{
    public class Consulta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConsultaId { get; set; }

        [Required]
        public DateTime DataHoraIncial { get; set; }

        [Required]
        public string HoraInicio { get; set; }

        [Required]
        public DateTime DataHoraFinal { get; set; }

        [Required]
        public string HoraFinal { get; set; }
    }
}
