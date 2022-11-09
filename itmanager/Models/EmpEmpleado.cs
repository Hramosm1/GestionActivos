using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class EmpEmpleado
    {
        public EmpEmpleado()
        {
            AceActivoEmpleados = new HashSet<AceActivoEmpleado>();
        }

        public long EmpId { get; set; }
        public string? EmpNombre { get; set; }
        public string? EmpCargo { get; set; }
        public string? EmpImagen { get; set; }
        public string? EmpExtension { get; set; }
        public string? EmpCorreo { get; set; }
        public string? EmpArea { get; set; }
        public string? EmpEstado { get; set; }

        public virtual ICollection<AceActivoEmpleado> AceActivoEmpleados { get; set; }
    }
}
