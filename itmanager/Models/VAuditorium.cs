using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class VAuditorium
    {
        public long? EmpId { get; set; }
        public string? AudUid { get; set; }
        public DateTime AudFecha { get; set; }
        public long ActId { get; set; }
        public string? AudEstadoAnterior { get; set; }
        public string? AudEstadoNuevo { get; set; }
        public string? AudModelo { get; set; }
        public string? AudSerie { get; set; }
        public string? EmpNombre { get; set; }
        public string? UsuModificadoPor { get; set; }
        public DateTime? ConFecha { get; set; }
        public string? ConId { get; set; }
        public string AudAñomes { get; set; } = null!;
    }
}
