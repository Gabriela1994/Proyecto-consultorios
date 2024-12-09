using System;
using System.Collections.Generic;

namespace DataAccess.Data
{
    public partial class VwTurno
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = null!;
        public int EstadoId { get; set; }
        public string? Observaciones { get; set; }
        public string Paciente { get; set; } = null!;
        public string PacienteTelefono { get; set; } = null!;
        public string PacienteEmail { get; set; } = null!;
        public string PacienteDni { get; set; } = null!;
        public string Medico { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string EstadoClase { get; set; } = null!;
        public string EstadoIcono { get; set; } = null!;
        public int EspecialidadId { get; set; }
        public string Especialidad { get; set; } = null!;
    }
}
