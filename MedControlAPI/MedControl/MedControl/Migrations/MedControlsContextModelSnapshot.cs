﻿// <auto-generated />
using System;
using MedControl.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MedControl.Migrations
{
    [DbContext(typeof(MedControlsContext))]
    partial class MedControlsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MedControl.Entities.Compromisso", b =>
                {
                    b.Property<int>("CompromissosId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ConsultaId");

                    b.Property<string>("Observacao");

                    b.Property<int?>("PacienterId");

                    b.HasKey("CompromissosId");

                    b.HasIndex("ConsultaId");

                    b.HasIndex("PacienterId");

                    b.ToTable("Compromisso");
                });

            modelBuilder.Entity("MedControl.Entities.Consulta", b =>
                {
                    b.Property<int>("ConsultaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataHoraFinal");

                    b.Property<string>("HoraInicio");

                    b.Property<DateTime>("DataHoraIncial");

                    b.Property<string>("HoraFinal");

                    b.HasKey("ConsultaId");

                    b.ToTable("Consulta");
                });

            modelBuilder.Entity("MedControl.Entities.Paciente", b =>
                {
                    b.Property<int>("PacienteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("PacienteId");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("MedControl.Entities.Compromisso", b =>
                {
                    b.HasOne("MedControl.Entities.Consulta", "Consulta")
                        .WithMany()
                        .HasForeignKey("ConsultaId");

                    b.HasOne("MedControl.Entities.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienterId");
                });
#pragma warning restore 612, 618
        }
    }
}
