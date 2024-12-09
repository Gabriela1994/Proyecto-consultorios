using System;
using System.Collections.Generic;

namespace DataAccess.Data
{
    public partial class Turno
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int EstadoId { get; set; }
        public string? Observaciones { get; set; }
    }
}
