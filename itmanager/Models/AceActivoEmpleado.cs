using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class AceActivoEmpleado
    {
        public long EmpId { get; set; }
        public long ActId { get; set; }
        public DateTime AceFecha { get; set; }
        public int? AceTipoAsignacion { get; set; }

        public virtual ActActivo Act { get; set; } = null!;
        public virtual EmpEmpleado Emp { get; set; } = null!;
    }
}
