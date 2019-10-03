using MedControl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedControl
{
    public static class MedControlContextExtensions
    {
        public static void SemeiaDadosParaDBContext(this MedControlsContext contexto)
        {
            if (contexto.Compromisso.Any())
                return;

            var compromissos = new List<Compromisso>()
            {
                new Compromisso()
                {
                      Consulta = new Consulta()
                         {
                              DataHoraFinal = DateTime.Now.AddHours(1),
                              HoraInicio = "11:30",
                              DataHoraIncial = DateTime.Now,
                              HoraFinal = "12:30",
                         },
                      Paciente = new Paciente()
                        {
                             Nome = "MARCELLO AZEVEDO",
                            DataNascimento = DateTime.Now.AddYears(-38)
                        },
                       Observacao = $" Data hora inicio tratamento {DateTime.Now} e Data hora fim tratamento {DateTime.Now.AddHours(1).ToString() }"
                }
            };
            
            contexto.Compromisso.AddRange(compromissos);
            contexto.SaveChanges();
        }
    }
}
