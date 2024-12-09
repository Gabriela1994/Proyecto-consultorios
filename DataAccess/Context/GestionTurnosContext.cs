using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Data
{
    public partial class GestionTurnosContext : DbContext
    {
        public GestionTurnosContext()
        {
        }

        public GestionTurnosContext(DbContextOptions<GestionTurnosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Especialidad> Especialidades { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Medico> Medicos { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;
        public virtual DbSet<Turno> Turnos { get; set; } = null!;
        public virtual DbSet<VwTurno> VwTurnos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.ToTable("Especialidad");

                entity.HasIndex(e => e.Nombre, "IX_Especialidad");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("Estado");

                entity.HasIndex(e => e.Nombre, "IX_Estado");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Clase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clase");

                entity.Property(e => e.Icono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("icono");

            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.ToTable("Medico");

                entity.HasIndex(e => new { e.Apellido, e.Nombre }, "IX_Medico_ApeNom");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Dni)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dni");

                entity.Property(e => e.EspecialidadId).HasColumnName("especialidadId");

                entity.Property(e => e.FechaAltaLaboral)
                    .HasColumnType("date")
                    .HasColumnName("fecha_alta_laboral");

                entity.Property(e => e.HorarioAtencionFin).HasColumnName("horario_atencion_fin");

                entity.Property(e => e.HorarioAtencionInicio).HasColumnName("horario_atencion_inicio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("Paciente");

                entity.HasIndex(e => e.Dni, "IX_DNI");

                entity.HasIndex(e => new { e.Apellido, e.Nombre }, "IX_Paciente_ApeNom");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Dni)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dni");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.ToTable("Turno");

                entity.HasIndex(e => new { e.MedicoId, e.Fecha, e.Hora }, "IX_Medico_Fecha_Hora");

                entity.HasIndex(e => new { e.PacienteId, e.Fecha, e.Hora }, "IX_Paciente_Fecha_Hora");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EstadoId).HasColumnName("estadoId");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora).HasColumnName("hora");

                entity.Property(e => e.MedicoId).HasColumnName("medicoId");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("observaciones");

                entity.Property(e => e.PacienteId).HasColumnName("pacienteId");
            });

            modelBuilder.Entity<VwTurno>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTurno");

                entity.Property(e => e.EstadoId).HasColumnName("estadoId");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora).HasColumnName("hora");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Medico)
                    .HasMaxLength(302)
                    .IsUnicode(false)
                    .HasColumnName("medico");

                entity.Property(e => e.MedicoId).HasColumnName("medicoId");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("observaciones");

                entity.Property(e => e.Paciente)
                    .HasMaxLength(302)
                    .IsUnicode(false)
                    .HasColumnName("paciente");

                entity.Property(e => e.PacienteEmail)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("paciente_email");

                entity.Property(e => e.PacienteId).HasColumnName("pacienteId");

                entity.Property(e => e.PacienteTelefono)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("paciente_telefono");
                
                entity.Property(e => e.PacienteDni)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("paciente_dni");

                entity.Property(e => e.Estado)
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("estado");

                entity.Property(e => e.EstadoClase)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("estado_clase");

                entity.Property(e => e.EstadoIcono)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("estado_icono");

                entity.Property(e => e.EspecialidadId).HasColumnName("especialidadId");

                entity.Property(e => e.Especialidad)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("especialidad");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
