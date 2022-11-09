using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class AcuActivoUsuario
    {
        public long UsuId { get; set; }
        public long ActId { get; set; }
        public DateTime? AcuFecha { get; set; }

        public virtual ActActivo Act { get; set; } = null!;
        public virtual UsuUsuario Usu { get; set; } = null!;
    }
}
