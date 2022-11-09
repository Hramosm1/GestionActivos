using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class UsuUsuario
    {
        public UsuUsuario()
        {
            AcuActivoUsuarios = new HashSet<AcuActivoUsuario>();
            UroUsuarioRols = new HashSet<UroUsuarioRol>();
        }

        /// <summary>
        /// Id del Usuario
        /// </summary>
        public long UsuId { get; set; }
        public string UsuNombre { get; set; } = null!;
        public DateTime UsuFechaFinal { get; set; }
        public string UsuLogin { get; set; } = null!;
        public string? UsuClave { get; set; }
        public bool UsuAdmin { get; set; }
        public bool UsuBloqueado { get; set; }
        public string? UsuImagen { get; set; }
        public bool UsuAsignaActivos { get; set; }

        public virtual ICollection<AcuActivoUsuario> AcuActivoUsuarios { get; set; }
        public virtual ICollection<UroUsuarioRol> UroUsuarioRols { get; set; }
    }
}
