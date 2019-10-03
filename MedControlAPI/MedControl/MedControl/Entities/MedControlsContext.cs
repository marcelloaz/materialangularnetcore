using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedControl.Entities
{
    public class MedControlsContext : DbContext
    {
        public MedControlsContext(DbContextOptions<MedControlsContext> options)
           : base(options)
        {
            
           // Database.Migrate();
        }

        public DbSet<Compromisso> Compromisso { get; set; }
        public DbSet<Consulta> Consulta { get; set; }
        public DbSet<Paciente> Paciente { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
