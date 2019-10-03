using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedControl.Dtos;
using MedControl.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MedControl.Controllers
{
    [Route("api/compromissos")]
    public class CompromissosController : Controller
    {
        private ICompromissoRepository _compromissoRepository;
        public CompromissosController(ICompromissoRepository compromissoRepository)
        {
            _compromissoRepository = compromissoRepository;
        }

        [HttpGet()]
        public IActionResult GetCompromissos()
        {
            var compromissoEntity = _compromissoRepository.GetCompromissos();
            var results = Mapper.Map<IEnumerable<Compromisso>>(compromissoEntity);

            return Ok(results);
        }

        
        [HttpPut]
        public async Task<IActionResult> PutCompromisso([FromBody] Compromisso compromissor)
        {
            if (compromissor == null)
            {
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var compromissoEntity = Mapper.Map<Entities.Compromisso>(compromissor);

            ForataHoraMinutoDataInicialDataFinalConsultaMedica(compromissoEntity);

            await _compromissoRepository.UpdateCompromisso(compromissoEntity);

            if (!await _compromissoRepository.SaveAsync())
            {
                throw new Exception("Atualização do compromisso falhou.");
            }

            return NoContent();
        }

 
        [HttpPost]
        public async Task<IActionResult> PostCompromisso([FromBody] Compromisso compromissor)
        {
            return await AddCompromissoMedico(compromissor);
        }

        public async Task<IActionResult> AddCompromissoMedico<T>(T compromisso) where T : class
        {
            if (compromisso == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var compromissoEntity = Mapper.Map<Entities.Compromisso>(compromisso);

            ForataHoraMinutoDataInicialDataFinalConsultaMedica(compromissoEntity);

            await _compromissoRepository.AddCompromisso(compromissoEntity);

            if (!await _compromissoRepository.SaveAsync())
            {
                throw new Exception("Ocorreu um erro ao tentar salvar.");
            }

            var compromissoReturnado = Mapper.Map<Compromisso>(compromissoEntity);

            return CreatedAtRoute("GetCompromissos",
                new { tourId = compromissoReturnado.CompromissosId },
                compromissoReturnado);
        }

        private static void ForataHoraMinutoDataInicialDataFinalConsultaMedica(Entities.Compromisso compromissoEntity)
        {
            var dataHoraInicial = compromissoEntity.Consulta.DataHoraIncial.Date.
                              AddHours(Convert.ToDouble(compromissoEntity.Consulta.HoraInicio.Split(":")[0])).
                              AddMinutes(Convert.ToDouble(compromissoEntity.Consulta.HoraInicio.Split(":")[1]));

            var dataHoraFinal = compromissoEntity.Consulta.DataHoraFinal.Date.
                             AddHours(Convert.ToDouble(compromissoEntity.Consulta.HoraFinal.Split(":")[0])).
                             AddMinutes(Convert.ToDouble(compromissoEntity.Consulta.HoraFinal.Split(":")[1]));

            compromissoEntity.Consulta.DataHoraIncial = dataHoraInicial;
            compromissoEntity.Consulta.DataHoraFinal = dataHoraFinal;
        }
    }
}