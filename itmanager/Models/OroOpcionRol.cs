using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class OroOpcionRol
    {
        public long RolId { get; set; }
        public long OpcId { get; set; }
        public DateTime OroDate { get; set; }

        public virtual OpcOpcion Opc { get; set; } = null!;
        public virtual RolRole Rol { get; set; } = null!;
    }
}
