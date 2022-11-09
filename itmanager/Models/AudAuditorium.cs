using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class AudAuditorium
    {
        public long AudId { get; set; }
        public DateTime AudFecha { get; set; }
        public long ActId { get; set; }
        public string? AudUid { get; set; }
        public string? AudEstadoAnterior { get; set; }
        public string? AudEstadoNuevo { get; set; }
        public string? AudModelo { get; set; }
        public string? AudSerie { get; set; }
        public long? EmpId { get; set; }
        public string? EmpNombre { get; set; }
        public string? EmpTipoAsignacion { get; set; }
        public string? UsuModificadoPor { get; set; }
        public string? ConId { get; set; }
        public string? AudTipoPersona { get; set; }
        public string AudAñomes { get; set; } = null!;

        public virtual ActActivo Act { get; set; } = null!;
        public virtual ConConfirmacion? Con { get; set; }
    }
}
