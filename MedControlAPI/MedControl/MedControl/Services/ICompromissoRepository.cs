using MedControl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedControl.Services
{
    public interface ICompromissoRepository
    {
        bool CompromissoExiste(int cityId);
        IEnumerable<Compromisso> GetCompromissos();
        Task<Compromisso> GetCompromisso(int compromissoId);
        Task UpdateCompromisso(Compromisso compromisso);
        Task AddCompromisso(Compromisso compromisso);
        Task DeleteCompromisso(Compromisso compromisso);
        bool Save();
        Task<bool> SaveAsync();
    }
}
