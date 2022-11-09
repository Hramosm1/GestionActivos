using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class SisSistema
    {
        public long SisId { get; set; }
        public string? SisNombre { get; set; }
        public string? SisDescripcion { get; set; }
        public string? SisProceso { get; set; }
        public int? SisTipo { get; set; }
    }
}
