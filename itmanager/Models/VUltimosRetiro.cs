using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class VUltimosRetiro
    {
        public DateTime AudFecha { get; set; }
        public string? EmpNombre { get; set; }
        public string? EmpTipoAsignacion { get; set; }
        public string? AudTipoPersona { get; set; }
        public string? AudEstadoAnterior { get; set; }
        public string? AudEstadoNuevo { get; set; }
        public string? AudModelo { get; set; }
        public string? AudUid { get; set; }
        public long ActId { get; set; }
        public int? Diffm { get; set; }
        public int? Diffh { get; set; }
        public int? Diffd { get; set; }
        public string? Tiempo { get; set; }
        public string TacNombre { get; set; } = null!;
        public string? AudSerie { get; set; }
    }
}
