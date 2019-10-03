using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedControl.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedControl.Services
{
    public class CompromissoRepository : ICompromissoRepository
    {
        private MedControlsContext _contexto;
        public CompromissoRepository(MedControlsContext contexto)
        {
            _contexto = contexto;
        }

        public async Task AddCompromisso(Compromisso compromisso)
        {
            await _contexto.Compromisso.AddAsync(compromisso);
        }

        public bool CompromissoExiste(int cityId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCompromisso(Compromisso compromisso)
        {
            _contexto.Compromisso.Remove(compromisso);
        }

        public async Task<Compromisso> GetCompromisso(int compromissoId)
        {
           return await _contexto.Compromisso
                    .Where(c => c.CompromissosId == compromissoId).FirstOrDefaultAsync();
        }

        public IEnumerable<Compromisso> GetCompromissos()
        {
            return _contexto.Compromisso.OrderBy(c => c.Paciente.Nome).ToList();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _contexto.SaveChangesAsync() >= 0);
        }

        public async Task UpdateCompromisso(Compromisso compromisso)
        {
             _contexto.Update(compromisso);
        }

        private IActionResult BadRequest()
        {
            throw new NotImplementedException();
        }
    }
}
