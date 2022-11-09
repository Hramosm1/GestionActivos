using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class ParParametro
    {
        public long ParId { get; set; }
        public string? ParNombre { get; set; }
        public string? ParValor1 { get; set; }
        public string? ParValor2 { get; set; }
        public long? ParTipo { get; set; }

        public virtual TipTipoParametro? ParTipoNavigation { get; set; }
    }
}
