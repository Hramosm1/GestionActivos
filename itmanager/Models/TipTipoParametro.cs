using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class TipTipoParametro
    {
        public TipTipoParametro()
        {
            ParParametros = new HashSet<ParParametro>();
        }

        public long TipId { get; set; }
        public string? TipNombre { get; set; }
        public int? TipOrden { get; set; }

        public virtual ICollection<ParParametro> ParParametros { get; set; }
    }
}
