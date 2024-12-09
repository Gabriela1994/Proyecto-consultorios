using System;
using System.Collections.Generic;

namespace DataAccess.Data
{
    public partial class Estado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clase { get; set; } = null!;
        public string Icono { get; set; } = null!;
    }
}
