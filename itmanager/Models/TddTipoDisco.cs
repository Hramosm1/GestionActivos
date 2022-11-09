using System;
using System.Collections.Generic;

namespace itmanager.Models
{
    public partial class TddTipoDisco
    {
        public TddTipoDisco()
        {
            DdDiscoDuros = new HashSet<DdDiscoDuro>();
        }

        public long TddId { get; set; }
        public string TddNombre { get; set; } = null!;

        public virtual ICollection<DdDiscoDuro> DdDiscoDuros { get; set; }
    }
}
