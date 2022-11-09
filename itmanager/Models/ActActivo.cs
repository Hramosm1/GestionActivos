using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class ActActivo
    {
        public ActActivo()
        {
            AceActivoEmpleados = new HashSet<AceActivoEmpleado>();
            AcuActivoUsuarios = new HashSet<AcuActivoUsuario>();
            AudAuditoria = new HashSet<AudAuditorium>();
            DdDiscoDuros = new HashSet<DdDiscoDuro>();
            DinDetalleInventarios = new HashSet<DinDetalleInventario>();
            HwHardwareSpecs = new HashSet<HwHardwareSpec>();
        }

        public long ActId { get; set; }
        public string ActModelo { get; set; } = null!;
        public string? ActSerie { get; set; }
        public string? ActObservaciones { get; set; }
        public string? ActEstado { get; set; }
        public bool ActInactivo { get; set; }
        public string? ActImagen1 { get; set; }
        public string? ActImagen2 { get; set; }
        public DateTime? ActFechacompra { get; set; }
        public string? ActNrocompra { get; set; }
        public string? ActProveedorcompra { get; set; }
        public string? ActUid { get; set; }
        public string? ActCondicion { get; set; }
        public string? ActLicencia { get; set; }
        public bool ActDadodebaja { get; set; }
        public long? TacId { get; set; }
        public long? TfaId { get; set; }
        public string? ActDdbComentario { get; set; }
        public string? ActDdbCausal { get; set; }

        public virtual TacTipoActivo? Tac { get; set; }
        public virtual TfaTipoFabricante? Tfa { get; set; }
        public virtual ICollection<AceActivoEmpleado> AceActivoEmpleados { get; set; }
        public virtual ICollection<AcuActivoUsuario> AcuActivoUsuarios { get; set; }
        public virtual ICollection<AudAuditorium> AudAuditoria { get; set; }
        public virtual ICollection<DdDiscoDuro> DdDiscoDuros { get; set; }
        public virtual ICollection<DinDetalleInventario> DinDetalleInventarios { get; set; }
        public virtual ICollection<HwHardwareSpec> HwHardwareSpecs { get; set; }
    }
}
