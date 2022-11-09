using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class TprTipoProcesador
    {
        public TprTipoProcesador()
        {
            HwHardwareSpecs = new HashSet<HwHardwareSpec>();
        }

        public long TprId { get; set; }
        public string? TprNombre { get; set; }
        public string? TprImagen { get; set; }

        public virtual ICollection<HwHardwareSpec> HwHardwareSpecs { get; set; }
    }
}
