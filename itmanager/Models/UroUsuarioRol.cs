using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class UroUsuarioRol
    {
        public long UsuId { get; set; }
        public long RolId { get; set; }
        public DateTime UroDate { get; set; }

        public virtual RolRole Rol { get; set; } = null!;
        public virtual UsuUsuario Usu { get; set; } = null!;
    }
}
