using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class DinDetalleInventario
    {
        public long DinId { get; set; }
        public long InvId { get; set; }
        public long ActId { get; set; }
        public string ActUid { get; set; } = null!;
        public DateTime DinFecha { get; set; }
        public string? DinEstado { get; set; }
        public string? DinUsuario { get; set; }
        public string? DinComentarios { get; set; }

        public virtual ActActivo Act { get; set; } = null!;
        public virtual InvInventario Inv { get; set; } = null!;
    }
}
