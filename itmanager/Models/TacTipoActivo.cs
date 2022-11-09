using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class TacTipoActivo
    {
        public TacTipoActivo()
        {
            ActActivos = new HashSet<ActActivo>();
        }

        public long TacId { get; set; }
        public string TacNombre { get; set; } = null!;
        public int TacInvMin { get; set; }

        public virtual ICollection<ActActivo> ActActivos { get; set; }
    }
}
