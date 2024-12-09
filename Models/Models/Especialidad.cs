using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Data
{
    public partial class Especialidad
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Nombre { get; set; } = null!;
    }
}
