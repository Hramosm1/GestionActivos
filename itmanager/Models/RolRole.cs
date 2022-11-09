using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class RolRole
    {
        public RolRole()
        {
            OroOpcionRols = new HashSet<OroOpcionRol>();
            UroUsuarioRols = new HashSet<UroUsuarioRol>();
        }

        public long RolId { get; set; }
        public string RolNombre { get; set; } = null!;
        public string? RolDescripcion { get; set; }
        public int RolBloqueado { get; set; }

        public virtual ICollection<OroOpcionRol> OroOpcionRols { get; set; }
        public virtual ICollection<UroUsuarioRol> UroUsuarioRols { get; set; }
    }
}
