using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class OpcOpcion
    {
        public OpcOpcion()
        {
            OroOpcionRols = new HashSet<OroOpcionRol>();
        }

        public long OpcId { get; set; }
        public string? OpcNombre { get; set; }
        public string? OpcNombreModulo { get; set; }
        public string? OpcDescripcion { get; set; }
        public string? OpcNombreObject { get; set; }
        public string? OpcLogo { get; set; }
        public long? OpcOrden { get; set; }
        public int? OpcPadre { get; set; }
        public int? OpcDeshabilitadoSistema { get; set; }

        public virtual ICollection<OroOpcionRol> OroOpcionRols { get; set; }
    }
}
