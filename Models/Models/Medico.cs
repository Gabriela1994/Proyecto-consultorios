using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DataAccess.Data
{
    public partial class Medico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Apellido is required")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "Nombre is required")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Telefono is required")]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "Direccion is required")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "Dni is required")]
        public string Dni { get; set; } = null!;

        [Required(ErrorMessage = "Especialidad is required")]
        public int EspecialidadId { get; set; }

        [Display(Name = "Fecha de alta laboral")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaAltaLaboral { get; set; }

        [Display(Name = "Hora de inicio de atención")]
        public TimeSpan HorarioAtencionInicio { get; set; }

        [Display(Name = "Hora de fin de atención")]
        public TimeSpan HorarioAtencionFin { get; set; }

        public Medico()
        {

        }
        public Medico(TimeSpan horarioInicio, TimeSpan horarioFinal)
        {
            HorarioAtencionInicio = horarioInicio;
            HorarioAtencionFin = horarioFinal;
        }

    }
}
