using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class VInventarioTipo
    {
        public string TacNombre { get; set; } = null!;
        public int? ActTotal { get; set; }
        public string? ActEstado { get; set; }
        public string EstGrupo { get; set; } = null!;
    }
}
