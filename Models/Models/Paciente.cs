using System;
using System.Collections.Generic;

namespace DataAccess.Data
{
    public partial class Paciente
    {
        public int Id { get; set; }
        public string Apellido { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Dni { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string NombreCompletoPaciente()
        {
            return $"{Nombre} {Apellido}";
        }

        public int EdadDelPaciente(DateTime fecha_nacimiento)
        {
            FechaNacimiento = fecha_nacimiento;

            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - fecha_nacimiento.Year;

            // Si el paciente aún no ha cumplido años en este año, resta 1
            if (fechaActual < fecha_nacimiento.AddYears(edad))
            {
                edad--;
            }

            return edad;
        }
    }


}
