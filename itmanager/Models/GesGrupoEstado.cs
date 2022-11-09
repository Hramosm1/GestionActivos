using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class GesGrupoEstado
    {
        public string EstEstado { get; set; } = null!;
        public string EstGrupo { get; set; } = null!;
        public int? EstAccAsigna { get; set; }
        public int EstInactivo { get; set; }
    }
}
