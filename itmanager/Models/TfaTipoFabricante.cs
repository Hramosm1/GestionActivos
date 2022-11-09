using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class TfaTipoFabricante
    {
        public TfaTipoFabricante()
        {
            ActActivos = new HashSet<ActActivo>();
        }

        public long TfaId { get; set; }
        public string? TfaNombre { get; set; }

        public virtual ICollection<ActActivo> ActActivos { get; set; }
    }
}
